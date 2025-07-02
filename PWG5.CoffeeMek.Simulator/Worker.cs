using PWG5.CoffeeMek.Simulator.Models;
using System.Text.Json;
namespace PWG5.CoffeeMek.Simulator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Random _random = new();
        private readonly Dictionary<LocationEnum, ProductionStepEnum> _lastStepByLocation = new()
        {
            { LocationEnum.Italy, ProductionStepEnum.TestLine },
            { LocationEnum.Brazil, ProductionStepEnum.TestLine },
            { LocationEnum.Vietnam, ProductionStepEnum.TestLine }
        };

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var locations = Enum.GetValues<LocationEnum>();
                var location = locations[_random.Next(locations.Length)];

                var lastStep = _lastStepByLocation[location];
                var nextStep = lastStep switch
                {
                    ProductionStepEnum.CNC => ProductionStepEnum.Lathe,
                    ProductionStepEnum.Lathe => ProductionStepEnum.AssemblyLine,
                    ProductionStepEnum.AssemblyLine => ProductionStepEnum.TestLine,
                    ProductionStepEnum.TestLine => ProductionStepEnum.CNC,
                    _=> ProductionStepEnum.CNC // Default case to avoid any issues
                };

                object payload = nextStep switch
                {
                    ProductionStepEnum.CNC => CNCPayloadFactory.Generate(),
                    ProductionStepEnum.Lathe => LathePayloadFactory.Generate(),
                    ProductionStepEnum.AssemblyLine => AssemblyPayloadFactory.Generate(),
                    ProductionStepEnum.TestLine => TestLinePayloadFactory.Generate(),
                    _ => throw new InvalidOperationException("Tipo di step non gestito")
                };

                bool isBlocked = _random.NextDouble() < 0.1; // 10% di probabilità di blocco
                string blockDescription = isBlocked ? "Errore macchina casuale" : string.Empty;

                var utcNow = DateTimeOffset.UtcNow;
                var tz = TimeZoneHelper.GetTimeZoneForLocation(location);
                var localNow = TimeZoneInfo.ConvertTime(utcNow, tz);

                var message = new ProductionMessage
                {
                    Location = location,
                    MachineStatus = isBlocked ? "Blocked" : "Running",
                    TimestampUtc = utcNow,
                    TimestampLocal = localNow,
                    IsMachineBlocked = isBlocked,
                    BlockDescription = blockDescription,
                    LastMaintenance = utcNow.AddDays(-_random.Next(1, 60)),
                    Payload = payload
                };
                _lastStepByLocation[location] = nextStep;

                try
                {
                    using var client = _httpClientFactory.CreateClient();
                    var json = JsonSerializer.Serialize(message, new JsonSerializerOptions 
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    //ROUTE NON PRESENTE PER MOTIVI DI SICUREZZA
                    //CHIAMATA A FUNCTION HTTP TRIGGER: (https://func-pwg5-coffeemek.azurewebsites.net/api/LogSenderAsync?code=REDDACTED)
                    var response = await client.PostAsync("", content, stoppingToken);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Messaggio di produzione inviato con successo per {Location} alle {TimestampLocal}", location, localNow);
                    }
                    else
                    {
                        _logger.LogWarning("Invio del messaggio di produzione fallito per {Location}. Codice di stato: {StatusCode}", location, response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Errore durante la generazione del messaggio di produzione per {Location}", location);
                    continue; // Salta l'iterazione corrente in caso di errore
                }

                await Task.Delay(_random.Next(5000, 10000), stoppingToken);
            }
        }
    }
}

using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using PWG5.CoffeeMek.Data.Models;
using PWG5.CoffeeMek.Functions.Services;
using System.Net;
using PWG5.CoffeeMek.Simulator.Models;

namespace PWG5.CoffeeMek.Functions;

public class SimulatorQueueFunctions
{
    private readonly ILogger<SimulatorQueueFunctions> _logger;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly DatabaseService _databaseService;
    private readonly IHttpClientFactory _httpClientFactory;

    public SimulatorQueueFunctions(
        ILogger<SimulatorQueueFunctions> logger,
        ServiceBusSender serviceBusSender,
        DatabaseService databaseService,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _serviceBusSender = serviceBusSender;
        _databaseService = databaseService;
        _httpClientFactory = httpClientFactory;
    }

    [Function(nameof(LogSenderAsync))]
    public async Task<IActionResult> LogSenderAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# LogSenderAsync HTTP trigger function processed a request.");

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        try
        {
            var deserializedRequest = JsonSerializer.Deserialize<ProductionMessage>(requestBody);

            var message = new ServiceBusMessage(JsonSerializer.Serialize(deserializedRequest))
            {
                ContentType = "application/json"
            };
            await _serviceBusSender.SendMessageAsync(message);
            return new OkObjectResult("Message sent to queue successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading request body");
            return new BadRequestObjectResult("Invalid request body");
        }

    }

    [Function(nameof(LogReceiverAsync))]
    public async Task LogReceiverAsync([ServiceBusTrigger("%QueueName%", Connection = "ServiceBusConnString")] ServiceBusReceivedMessage message, CancellationToken cancellationToken)
    {
        try
        {
            var stringMessage = message.Body.ToString();
            var deserializedRequest = JsonSerializer.Deserialize<ProductionMessage>(stringMessage);

            if (deserializedRequest != null)
            {
                object parameters = new { LocationId = deserializedRequest.Location };
                var lotCode = await _databaseService.GetScalarAsync<string>("sp_GetProductionLotByLocation", parameters, cancellationToken);
                using var httpClient = _httpClientFactory.CreateClient("LoggingClient");

                string resultString;
                string endpoint;
                bool updateCompletedUnits = false;

                if (deserializedRequest.Payload is JsonElement payloadElement)
                {
                    var machineType = payloadElement.GetProperty("machineType").GetString();

                    switch (machineType)
                    {
                        case "CNC":
                            var cncLog = new CutterCNCLog()
                            {
                                LotCode = lotCode,
                                TimestampLocal = deserializedRequest.TimestampLocal,
                                TimestampUTC = deserializedRequest.TimestampUtc,
                                IsMachineBlocked = deserializedRequest.IsMachineBlocked,
                                BlockDescription = deserializedRequest.BlockDescription,
                                LastMaintenance = deserializedRequest.LastMaintenance,
                                LocationId = ((int)deserializedRequest.Location),
                                CycleTime = GetDateTimeOffSetFromSecondsFloat((float)payloadElement.GetProperty("cycleTime").GetDouble()),
                                CutDepth = payloadElement.GetProperty("cutDepth").GetDouble(),
                                Vibration = payloadElement.GetProperty("vibration").GetDouble(),
                                MachineStatus = deserializedRequest.MachineStatus
                            };
                            resultString = JsonSerializer.Serialize(cncLog);
                            endpoint = "cuttercnc";
                            break;

                        case "Lathe":
                            var latheLog = new LatheLog()
                            {
                                LotCode = lotCode,
                                TimestampLocal = deserializedRequest.TimestampLocal,
                                TimestampUTC = deserializedRequest.TimestampUtc,
                                IsMachineBlocked = deserializedRequest.IsMachineBlocked,
                                BlockDescription = deserializedRequest.BlockDescription,
                                LastMaintenance = deserializedRequest.LastMaintenance,
                                LocationId = ((int)deserializedRequest.Location),
                                RotationSpeed = payloadElement.GetProperty("rotationSpeed").GetInt32(),
                                SpindleTemperature = payloadElement.GetProperty("spindleTemperature").GetDouble(),
                                MachineStatus = deserializedRequest.MachineStatus
                            };
                            resultString = JsonSerializer.Serialize(latheLog);
                            endpoint = "lathe";
                            break;

                        case "AssemblyLine":
                            var assemblyLog = new AssemblyLineLog()
                            {
                                LotCode = lotCode,
                                TimestampLocal = deserializedRequest.TimestampLocal,
                                TimestampUTC = deserializedRequest.TimestampUtc,
                                IsMachineBlocked = deserializedRequest.IsMachineBlocked,
                                BlockDescription = deserializedRequest.BlockDescription,
                                LastMaintenance = deserializedRequest.LastMaintenance,
                                LocationId = ((int)deserializedRequest.Location),
                                MeanStationTime = GetDateTimeOffSetFromSecondsFloat((float)payloadElement.GetProperty("meanStationTime").GetDouble()),
                                MachineStatus = deserializedRequest.MachineStatus
                            };
                            resultString = JsonSerializer.Serialize(assemblyLog);
                            endpoint = "assemblyline";
                            break;

                        case "TestLine":
                            var testLineLog = new TestLineLog()
                            {
                                LotCode = lotCode,
                                TimestampLocal = deserializedRequest.TimestampLocal,
                                TimestampUTC = deserializedRequest.TimestampUtc,
                                IsMachineBlocked = deserializedRequest.IsMachineBlocked,
                                BlockDescription = deserializedRequest.BlockDescription,
                                LastMaintenance = deserializedRequest.LastMaintenance,
                                LocationId = ((int)deserializedRequest.Location),
                                TestResult = payloadElement.GetProperty("testResult").GetString(),
                                BoilerPressure = payloadElement.GetProperty("boilerPressure").GetDouble(),
                                BoilerTemperature = payloadElement.GetProperty("boilerTemperature").GetDouble(),
                                EnergyConsumption = payloadElement.GetProperty("energyConsumption").GetDouble()
                            };
                            resultString = JsonSerializer.Serialize(testLineLog);
                            endpoint = "testline";
                            updateCompletedUnits = true;
                            break;

                        default:
                            throw new InvalidOperationException("Unknown machine type");
                    }

                    await httpClient.PostAsync(endpoint,
                        new StringContent(resultString, System.Text.Encoding.UTF8, "application/json"));

                    if (updateCompletedUnits)
                    {
                        parameters = new { LotCode = lotCode };
                        await _databaseService.ExecuteAsync("sp_IncreaseProducedItemsCount", parameters, cancellationToken);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading message body");
        }
    }

    private DateTimeOffset GetDateTimeOffSetFromSecondsFloat(float seconds)
    {
        var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        return epoch.AddSeconds(seconds);
    }
}
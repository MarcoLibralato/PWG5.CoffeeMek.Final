using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PWG5.CoffeeMek.Functions.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddHttpClient("LoggingClient", client =>
{
    client.BaseAddress = new Uri("https://apiservice.victoriouspebble-e8ef726a.italynorth.azurecontainerapps.io/api/v1/");
    client.DefaultRequestHeaders.Add("User-Agent", "MyAzureFunctionApp");
});

builder.Services.AddSingleton(provider => {
    var serviceBusConnString = Environment.GetEnvironmentVariable("ServiceBusConnString");
    if (string.IsNullOrEmpty(serviceBusConnString))
    {
        throw new InvalidOperationException("ServiceBusConnectionString environment variable not set in configuration.");
    }
    return new ServiceBusClient(serviceBusConnString);
});

builder.Services.AddSingleton(provider => {
    var serviceBusClient = provider.GetRequiredService<ServiceBusClient>();
    var queueName = Environment.GetEnvironmentVariable("QueueName");
    if (string.IsNullOrEmpty(queueName))
    {
        throw new InvalidOperationException("ServiceBusQueueName environment variable not set in configuration.");
    }
    return serviceBusClient.CreateSender(queueName);
});

builder.Services.AddScoped<DatabaseService>(provider =>
{
    var connString = Environment.GetEnvironmentVariable("DbConnString");
    if (string.IsNullOrEmpty(connString))
    {
        throw new InvalidOperationException("DbConnString environment variable not set in configuration.");
    }
    return new DatabaseService(connString);
});


builder.AddServiceDefaults();

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PWG5.CoffeeMek.Simulator;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddHostedService<Worker>();

// Configura il logging per scrivere in console
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var host = builder.Build();
host.Run();

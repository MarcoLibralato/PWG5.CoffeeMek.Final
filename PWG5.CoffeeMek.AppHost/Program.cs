var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var azureSql = builder.AddConnectionString("database");

var apiService = builder.AddProject<Projects.PWG5_CoffeeMek_ApiService>("apiservice").WithReference(azureSql).WithExternalHttpEndpoints();

builder.AddProject<Projects.PWG5_CoffeeMek_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.PWG5_CoffeeMek_Simulator>("pwg5-coffeemek-simulator");

builder.Build().Run();

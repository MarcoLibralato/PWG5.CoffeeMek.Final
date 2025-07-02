using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PWG5.CoffeeMek.ApiService;
using PWG5.CoffeeMek.ApiService.Endpoints;
using PWG5.CoffeeMek.ApiService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IAssemblyLineLogService, AssemblyLineLogService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICutterCNCLogService, CutterCNCLogService>();
builder.Services.AddScoped<ILatheLogService, LatheLogService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<ITestLineLogService, TestLineLogService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("database"), x => x.MigrationsHistoryTable("__EFMigrationsHistory", "coffee_mek")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAssemblyLineEndpoints();
app.MapCustomerEndpoints();
app.MapCutterCNCEndpoints();
app.MapLatheEndpoints();
app.MapLocationEndpoints();
app.MapLotEndpoints();
app.MapTestLineEndpoints();


app.Run();


using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class LatheEndpoints
{
    public static IEndpointRouteBuilder MapLatheEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/lathe")
            .WithTags("Lathe")
            .WithOpenApi();
        group.MapGet("/", GetLatheLogsAsync);
        group.MapGet("/{id:int}", GetLatheLogByIdAsync);
        group.MapPost("/", CreateLatheLogAsync);
        return app;
    }
    public static async Task<IResult> GetLatheLogsAsync(ILatheLogService latheLogService)
    {
        try
        {
            var latheLogs = await latheLogService.GetAllAsync();
            return Results.Ok(latheLogs);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetLatheLogByIdAsync(int id, ILatheLogService latheLogService)
    {
        try
        {
            var latheLog = await latheLogService.GetByIdAsync(id);
            if (latheLog == null)
            {
                return Results.NotFound($"Lathe log with ID {id} not found.");
            }
            return Results.Ok(latheLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> CreateLatheLogAsync(LatheLog latheLog, ILatheLogService latheLogService)
    {
        try
        {
            if (latheLog == null)
            {
                return Results.BadRequest("Lathe log cannot be null.");
            }
            var createdLatheLog = await latheLogService.CreateAsync(latheLog);
            return Results.Created($"/api/v1/lathe/{createdLatheLog.Id}", createdLatheLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}

using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class CutterCNCEndpoints
{
    public static IEndpointRouteBuilder MapCutterCNCEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/cuttercnc")
            .WithTags("CutterCNC")
            .WithOpenApi();
        group.MapGet("/", GetCutterCNCLogsAsync);
        group.MapGet("/{id:int}", GetCutterCNCLogByIdAsync);
        group.MapPost("/", CreateCutterCNCLogAsync);
        return app;
    }
    public static async Task<IResult> GetCutterCNCLogsAsync(ICutterCNCLogService cutterCNCLogService)
    {
        try
        {
            var cutterCNCLogs = await cutterCNCLogService.GetAllAsync();
            return Results.Ok(cutterCNCLogs);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetCutterCNCLogByIdAsync(int id, ICutterCNCLogService cutterCNCLogService)
    {
        try
        {
            var cutterCNCLog = await cutterCNCLogService.GetByIdAsync(id);
            if (cutterCNCLog == null)
            {
                return Results.NotFound($"Cutter CNC log with ID {id} not found.");
            }
            return Results.Ok(cutterCNCLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> CreateCutterCNCLogAsync(CutterCNCLog cutterCNCLog, ICutterCNCLogService cutterCNCLogService)
    {
        try
        {
            if (cutterCNCLog == null)
            {
                return Results.BadRequest("Cutter CNC log cannot be null.");
            }
            var createdCutterCNCLog = await cutterCNCLogService.CreateAsync(cutterCNCLog);
            return Results.Created($"/api/v1/cuttercnc/{createdCutterCNCLog.Id}", createdCutterCNCLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}

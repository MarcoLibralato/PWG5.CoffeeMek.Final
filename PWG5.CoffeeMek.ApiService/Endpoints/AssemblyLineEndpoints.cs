using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class AssemblyLineEndpoints
{
    public static IEndpointRouteBuilder MapAssemblyLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/assemblyline")
            .WithTags("AssemblyLine")
            .WithOpenApi();

        group.MapGet("/", GetAssemblyLineAsync);
        group.MapGet("/{id:int}", GetAssemblyLineByIdAsync);
        group.MapPost("/", CreateAssemblyLineAsync);

        return app;
    }

    public static async Task<IResult> GetAssemblyLineAsync(IAssemblyLineLogService assemblyLineLogService)
    {
        try
        {
            var assemblyLineLogs = await assemblyLineLogService.GetAllAsync();
            return Results.Ok(assemblyLineLogs);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetAssemblyLineByIdAsync(int id, IAssemblyLineLogService assemblyLineLogService)
    {
        try
        {
            var assemblyLineLog = await assemblyLineLogService.GetByIdAsync(id);
            if (assemblyLineLog == null)
            {
                return Results.NotFound($"Assembly line log with ID {id} not found.");
            }
            return Results.Ok(assemblyLineLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> CreateAssemblyLineAsync(AssemblyLineLog assemblyLineLog, IAssemblyLineLogService assemblyLineLogService)
    {
        try
        {
            if (assemblyLineLog == null)
            {
                return Results.BadRequest("Assembly line log cannot be null.");
            }
            var createdAssemblyLineLog = await assemblyLineLogService.CreateAsync(assemblyLineLog);
            return Results.Created($"/api/v1/assemblyline/{createdAssemblyLineLog.Id}", createdAssemblyLineLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}

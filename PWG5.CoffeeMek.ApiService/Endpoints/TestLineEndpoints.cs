using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class TestLineEndpoints
{
    public static IEndpointRouteBuilder MapTestLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/testline")
            .WithTags("TestLine")
            .WithOpenApi();
        group.MapGet("/", GetTestLinesAsync);
        group.MapGet("/{id:int}", GetTestLineByIdAsync);
        group.MapPost("/", CreateTestLineAsync);
        return app;
    }
    public static async Task<IResult> GetTestLinesAsync(ITestLineLogService testLineLogService)
    {
        try
        {
            var testLineLogs = await testLineLogService.GetAllAsync();
            return Results.Ok(testLineLogs);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetTestLineByIdAsync(int id, ITestLineLogService testLineLogService)
    {
        try
        {
            var testLineLog = await testLineLogService.GetByIdAsync(id);
            if (testLineLog == null)
            {
                return Results.NotFound($"Test line log with ID {id} not found.");
            }
            return Results.Ok(testLineLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> CreateTestLineAsync(TestLineLog testLineLog, ITestLineLogService testLineLogService)
    {
        try
        {
            if (testLineLog == null)
            {
                return Results.BadRequest("Test line log cannot be null.");
            }
            var createdTestLineLog = await testLineLogService.CreateAsync(testLineLog);
            return Results.Created($"/api/v1/testline/{createdTestLineLog.Id}", createdTestLineLog);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}

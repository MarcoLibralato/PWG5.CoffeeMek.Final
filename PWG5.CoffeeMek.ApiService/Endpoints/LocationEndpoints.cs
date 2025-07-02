using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class LocationEndpoints
{
    public static IEndpointRouteBuilder MapLocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/location")
            .WithTags("Location")
            .WithOpenApi();
        group.MapGet("/", GetLocationsAsync);
        group.MapGet("/{id:int}", GetLocationByIdAsync);
        return app;
    }
    public static async Task<IResult> GetLocationsAsync(ILocationService locationService)
    {
        try
        {
            var locations = await locationService.GetAllAsync();
            return Results.Ok(locations);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetLocationByIdAsync(int id, ILocationService locationService)
    {
        try
        {
            var location = await locationService.GetByIdAsync(id);
            if (location == null)
            {
                return Results.NotFound($"Location with ID {id} not found.");
            }
            return Results.Ok(location);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }

}
using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class LotEndpoints
{
    public static IEndpointRouteBuilder MapLotEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/lot")
            .WithTags("Lot")
            .WithOpenApi();
        group.MapGet("/", GetLotsAsync);
        group.MapGet("/{id}", GetLotByIdAsync);
        group.MapPost("/", CreateLotAsync);
        group.MapPut("/{id}", UpdateLotAsync);
        group.MapDelete("/{id}", DeleteLotAsync);
        return app;
    }
    public static async Task<IResult> GetLotsAsync(ILotService lotService)
    {
        try
        {
            var lots = await lotService.GetAllAsync();
            return Results.Ok(lots);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetLotByIdAsync(string id, ILotService lotService)
    {
        try
        {
            var lot = await lotService.GetByIdAsync(id);
            if (lot == null)
            {
                return Results.NotFound($"Lot with ID {id} not found.");
            }
            return Results.Ok(lot);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> CreateLotAsync(Lot lot, ILotService lotService)
    {
        try
        {
            if (lot == null)
            {
                return Results.BadRequest("Lot cannot be null.");
            }
            var createdLot = await lotService.CreateAsync(lot);
            return Results.Created($"/api/v1/lot/{createdLot.LotCode}", createdLot);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> UpdateLotAsync(string id, Lot lot, ILotService lotService)
    {
        try
        {
            if (lot == null)
            {
                return Results.BadRequest("Lot cannot be null.");
            }
            var updatedLot = await lotService.UpdateAsync(id, lot);
            if (!updatedLot)
            {
                return Results.NotFound($"Lot with ID {id} not found.");
            }
            return Results.Ok(updatedLot);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> DeleteLotAsync(string id, ILotService lotService)
    {
        try
        {
            var deleted = await lotService.DeleteAsync(id);
            if (!deleted)
            {
                return Results.NotFound($"Lot with ID {id} not found.");
            }
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}

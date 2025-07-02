using PWG5.CoffeeMek.ApiService.Services;
using PWG5.CoffeeMek.Data.Models;
namespace PWG5.CoffeeMek.ApiService.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/customer")
            .WithTags("Customer")
            .WithOpenApi();
        group.MapGet("/", GetCustomersAsync);
        group.MapGet("/{id:int}", GetCustomerByIdAsync);
        group.MapPost("/", CreateCustomerAsync);
        group.MapPut("/{id:int}", UpdateCustomerAsync);
        group.MapDelete("/{id:int}", DeleteCustomerAsync);
        return app;
    }
    public static async Task<IResult> GetCustomersAsync(ICustomerService customerService)
    {
        try
        {
            var customers = await customerService.GetAllAsync();
            return Results.Ok(customers);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> GetCustomerByIdAsync(int id, ICustomerService customerService)
    {
        try
        {
            var customer = await customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return Results.NotFound($"Customer with ID {id} not found.");
            }
            return Results.Ok(customer);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> CreateCustomerAsync(Customer customer, ICustomerService customerService)
    {
        try
        {
            if (customer == null)
            {
                return Results.BadRequest("Customer cannot be null.");
            }
            var createdCustomer = await customerService.CreateAsync(customer);
            return Results.Created($"/api/v1/customer/{createdCustomer.Id}", createdCustomer);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> UpdateCustomerAsync(int id, Customer customer, ICustomerService customerService)
    {
        try
        {
            if (customer == null)
            {
                return Results.BadRequest("Customer cannot be null.");
            }
            var updatedCustomer = await customerService.UpdateAsync(id, customer);
            if (!updatedCustomer)
            {
                return Results.NotFound($"Customer with ID {id} not found.");
            }
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
    public static async Task<IResult> DeleteCustomerAsync(int id, ICustomerService customerService)
    {
        try
        {
            var deletedCustomer = await customerService.DeleteAsync(id);
            if (!deletedCustomer)
            {
                return Results.NotFound($"Customer with ID {id} not found.");
            }
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}

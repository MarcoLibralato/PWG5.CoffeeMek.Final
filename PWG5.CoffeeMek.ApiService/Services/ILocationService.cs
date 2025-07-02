using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface ILocationService
    {
        Task<List<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(int id);
    }
}
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface ILatheLogService
    {
        Task<LatheLog> CreateAsync(LatheLog latheLog);
        Task<List<LatheLog>> GetAllAsync();
        Task<LatheLog?> GetByIdAsync(int id);
    }
}
using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface ITestLineLogService
    {
        Task<TestLineLog> CreateAsync(TestLineLog testLineLog);
        Task<List<TestLineLog>> GetAllAsync();
        Task<TestLineLog?> GetByIdAsync(int id);
    }
}
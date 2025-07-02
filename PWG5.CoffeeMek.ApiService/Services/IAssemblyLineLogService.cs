using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface IAssemblyLineLogService
    {
        Task<AssemblyLineLog> CreateAsync(AssemblyLineLog assemblyLineLog);
        Task<List<AssemblyLineLog>> GetAllAsync();
        Task<AssemblyLineLog?> GetByIdAsync(int id);
    }
}
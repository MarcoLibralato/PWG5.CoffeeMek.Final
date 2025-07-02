using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface ICutterCNCLogService
    {
        Task<CutterCNCLog> CreateAsync(CutterCNCLog cutterCNCLog);
        Task<List<CutterCNCLog>> GetAllAsync();
        Task<CutterCNCLog?> GetByIdAsync(int id);
    }
}
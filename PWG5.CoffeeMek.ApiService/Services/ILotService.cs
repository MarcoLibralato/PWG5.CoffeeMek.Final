using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface ILotService
    {
        Task<Lot> CreateAsync(Lot lot);
        Task<bool> DeleteAsync(string lotCode);
        Task<List<Lot>> GetAllAsync();
        Task<Lot?> GetByIdAsync(string lotCode);
        Task<bool> UpdateAsync(string lotCode, Lot lot);
    }
}
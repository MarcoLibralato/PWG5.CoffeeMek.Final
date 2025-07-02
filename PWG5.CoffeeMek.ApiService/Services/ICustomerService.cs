using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.ApiService.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Customer customer);
    }
}
using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
using System;

namespace PWG5.CoffeeMek.ApiService.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ILogger<CustomerService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FirstOrDefaultAsync(props => props.Id == id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateAsync(int id, Customer customer)
    {
        var existing = await _context.Customers.FindAsync(id);
        if (existing == null) return false;

        existing.Name = customer.Name;
        existing.Email = customer.Email;
        existing.Phone = customer.Phone;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var Customer = await _context.Customers.FindAsync(id);
        if (Customer == null) return false;

        _context.Customers.Remove(Customer);
        await _context.SaveChangesAsync();
        return true;
    }

}

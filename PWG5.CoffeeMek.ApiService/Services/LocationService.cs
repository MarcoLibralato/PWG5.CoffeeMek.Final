using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
using System;

namespace PWG5.CoffeeMek.ApiService.Services;

public class LocationService : ILocationService
{
    private readonly AppDbContext _context;
    private readonly ILogger<LocationService> _logger;

    public LocationService(ILogger<LocationService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location?> GetByIdAsync(int id)
    {
        return await _context.Locations.FirstOrDefaultAsync(props => props.Id == id);
    }
}

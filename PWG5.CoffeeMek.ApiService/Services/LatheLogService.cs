using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
using System;

namespace PWG5.CoffeeMek.ApiService.Services;

public class LatheLogService : ILatheLogService
{
    private readonly AppDbContext _context;
    private readonly ILogger<LatheLogService> _logger;

    public LatheLogService(ILogger<LatheLogService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<LatheLog>> GetAllAsync()
    {
        return await _context.LatheLogs.ToListAsync();
    }

    public async Task<LatheLog?> GetByIdAsync(int id)
    {
        return await _context.LatheLogs.FirstOrDefaultAsync(props => props.Id == id);
    }

    public async Task<LatheLog> CreateAsync(LatheLog latheLog)
    {
        _context.LatheLogs.Add(latheLog);
        await _context.SaveChangesAsync();
        return latheLog;
    }
}

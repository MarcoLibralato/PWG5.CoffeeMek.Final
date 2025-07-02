using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
using System;

namespace PWG5.CoffeeMek.ApiService.Services;

public class CutterCNCLogService : ICutterCNCLogService
{
    private readonly AppDbContext _context;
    private readonly ILogger<CutterCNCLogService> _logger;

    public CutterCNCLogService(ILogger<CutterCNCLogService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<CutterCNCLog>> GetAllAsync()
    {
        return await _context.CutterCNCLogs.ToListAsync();
    }

    public async Task<CutterCNCLog?> GetByIdAsync(int id)
    {
        return await _context.CutterCNCLogs.FirstOrDefaultAsync(props => props.Id == id);
    }

    public async Task<CutterCNCLog> CreateAsync(CutterCNCLog cutterCNCLog)
    {
        _context.CutterCNCLogs.Add(cutterCNCLog);
        await _context.SaveChangesAsync();
        return cutterCNCLog;
    }
}

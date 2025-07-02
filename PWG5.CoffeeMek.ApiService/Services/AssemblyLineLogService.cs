using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
using System;

namespace PWG5.CoffeeMek.ApiService.Services;

public class AssemblyLineLogService : IAssemblyLineLogService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AssemblyLineLogService> _logger;

    public AssemblyLineLogService(ILogger<AssemblyLineLogService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<AssemblyLineLog>> GetAllAsync()
    {
        return await _context.AssemblyLineLogs.ToListAsync();
    }

    public async Task<AssemblyLineLog?> GetByIdAsync(int id)
    {
        return await _context.AssemblyLineLogs.FirstOrDefaultAsync(props => props.Id == id);
    }

    public async Task<AssemblyLineLog> CreateAsync(AssemblyLineLog assemblyLineLog)
    {
        _context.AssemblyLineLogs.Add(assemblyLineLog);
        await _context.SaveChangesAsync();
        return assemblyLineLog;
    }
}

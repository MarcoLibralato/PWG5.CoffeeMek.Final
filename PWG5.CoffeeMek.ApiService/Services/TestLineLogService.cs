using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
using System;

namespace PWG5.CoffeeMek.ApiService.Services;

public class TestLineLogService : ITestLineLogService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TestLineLogService> _logger;

    public TestLineLogService(ILogger<TestLineLogService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<TestLineLog>> GetAllAsync()
    {
        return await _context.TestLineLogs.ToListAsync();
    }

    public async Task<TestLineLog?> GetByIdAsync(int id)
    {
        return await _context.TestLineLogs.FirstOrDefaultAsync(propa => propa.Id == id);
    }

    public async Task<TestLineLog> CreateAsync(TestLineLog testLineLog)
    {
        _context.TestLineLogs.Add(testLineLog);
        await _context.SaveChangesAsync();
        return testLineLog;
    }
}


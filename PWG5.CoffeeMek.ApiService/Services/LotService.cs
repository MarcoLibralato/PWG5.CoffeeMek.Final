using Microsoft.EntityFrameworkCore;
using PWG5.CoffeeMek.Data.Models;
namespace PWG5.CoffeeMek.ApiService.Services;

public class LotService : ILotService
{
    private readonly AppDbContext _context;
    private readonly ILogger<LotService> _logger;

    public LotService(ILogger<LotService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<List<Lot>> GetAllAsync()
    {
        return await _context.Lots.ToListAsync();
    }

    public async Task<Lot?> GetByIdAsync(string lotCode)
    {
        return await _context.Lots.FirstOrDefaultAsync(props => props.LotCode == lotCode);
    }

    public async Task<Lot> CreateAsync(Lot lot)
    {
        string prefix = lot.LocationId switch
        {
            3 => "ITA",
            2 => "BRA",
            1 => "VNM"
        };

        var maxExisting = await _context.Lots
        .Where(l => l.LotCode.StartsWith(prefix))
        .OrderByDescending(l => l.LotCode)
        .Select(l => l.LotCode)
        .FirstOrDefaultAsync();

        int nextNumber = 1;
        if (!string.IsNullOrEmpty(maxExisting) && maxExisting.Length > 3)
        {
            var numPart = maxExisting.Substring(3);
            if (int.TryParse(numPart, out int parsedNum))
            {
                nextNumber = parsedNum + 1;
            }
        }

        lot.LotCode = $"{prefix}{nextNumber.ToString("D3")}";
        _context.Lots.Add(lot);
        await _context.SaveChangesAsync();
        return lot;
    }

    public async Task<bool> UpdateAsync(string lotCode, Lot lot)
    {
        var existing = await _context.Lots.FindAsync(lotCode);
        if (existing == null) return false;

        existing.Quantity = lot.Quantity;
        existing.Status = lot.Status;
        existing.ProducedItems = lot.ProducedItems;
        existing.ProductionStarted = lot.ProductionStarted;
        existing.ProductionFinished = lot.ProductionFinished;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string lotCode)
    {
        var lot = await _context.Lots.FindAsync(lotCode);
        if (lot == null) return false;

        _context.Lots.Remove(lot);
        await _context.SaveChangesAsync();
        return true;
    }
}

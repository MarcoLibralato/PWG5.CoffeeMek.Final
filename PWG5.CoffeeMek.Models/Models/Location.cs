using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public class Location
{
    public int Id { get; set; }
    [Required]
    public required string Nation { get; set; }

    public ICollection<Lot> Lots { get; }
    public ICollection<CutterCNCLog> CutterCNCLogs { get; }
    public ICollection<LatheLog> LatheLogs { get; }
    public ICollection<AssemblyLineLog> AssemblyLineLogs { get; }
    public ICollection<TestLineLog> TestLineLogs { get; }

}

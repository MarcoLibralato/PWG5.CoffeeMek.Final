using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public abstract class MachineLog
{
    [Required]
    public required string LotCode { get; set; }
    [Required]
    public DateTimeOffset TimestampLocal { get; set; }
    [Required]
    public DateTimeOffset TimestampUTC { get; set; }
    public bool IsMachineBlocked { get; set; }
    public string? BlockDescription { get; set; }
    [Required]
    public DateTimeOffset LastMaintenance { get; set; }

    [Required]
    public int LocationId { get; set; }
    public Location Location { get; set; }
}

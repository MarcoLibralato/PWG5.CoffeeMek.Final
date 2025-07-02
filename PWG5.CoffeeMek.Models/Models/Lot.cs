using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public class Lot
{
    [Key]
    public string LotCode { get; set; }
    [Required]
    public DateTimeOffset ScheduledStartTime { get; set; }
    [Required]
    public uint Quantity { get; set; }
    [Required]
    public required string Status { get; set; }
    [Required]
    public uint ProducedItems { get; set; }
    [Required]
    public DateTimeOffset ProductionStarted { get; set; }
    [Required]
    public DateTimeOffset ProductionFinished { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    [Required]
    public int LocationId { get; set; }
    public Location Location { get; set; }
}
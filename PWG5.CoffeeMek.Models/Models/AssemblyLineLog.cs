using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public class AssemblyLineLog : MachineLog
{
    public int Id { get; set; }
    [Required]
    public DateTimeOffset MeanStationTime { get; set; }
    [Required]
    public required string MachineStatus { get; set; }
}

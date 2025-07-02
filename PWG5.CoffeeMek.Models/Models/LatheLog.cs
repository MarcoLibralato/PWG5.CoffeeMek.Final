using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public class LatheLog : MachineLog
{
    public int Id {  get; set; }
    [Required]
    public required string MachineStatus { get; set; }
    [Required]
    public int RotationSpeed {  get; set; }
    [Required]
    public double SpindleTemperature { get; set; }
  
}

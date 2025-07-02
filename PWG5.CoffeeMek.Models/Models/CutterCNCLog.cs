using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public class CutterCNCLog : MachineLog
{
    public int Id { get; set; }
    [Required]
    public DateTimeOffset CycleTime { get; set; }
    [Required]
    public double CutDepth { get; set; }
    [Required]
    public double Vibration { get; set; }
    [Required]
    public required string MachineStatus { get; set; }
}

using Bogus;
namespace PWG5.CoffeeMek.Simulator.Models;

public class CNCPayload
{
    public string MachineType => "CNC";
    public double CycleTime { get; set; }
    public double CutDepth { get; set; }
    public double Vibration { get; set; }

}
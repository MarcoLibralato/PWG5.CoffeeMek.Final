using Bogus;
namespace PWG5.CoffeeMek.Simulator.Models;

public class LathePayload
{
    public string MachineType => "Lathe";
    public int RotationSpeed { get; set; }
    public double SpindleTemperature { get; set; }

}

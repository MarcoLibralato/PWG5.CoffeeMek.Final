using Bogus;
namespace PWG5.CoffeeMek.Simulator.Models;

public class TestLinePayload
{
    public string MachineType => "TestLine";
    public string TestResult { get; set; }
    public double BoilerPressure { get; set; }
    public double BoilerTemperature { get; set; }
    public double EnergyConsumption { get; set; }

}
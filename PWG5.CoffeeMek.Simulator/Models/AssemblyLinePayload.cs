using Bogus;
namespace PWG5.CoffeeMek.Simulator.Models;

public class AssemblyLinePayload
{
    public string MachineType => "AssemblyLine";
    public double MeanStationTime { get; set; }
    //public string Anomalies { get; set; }

}

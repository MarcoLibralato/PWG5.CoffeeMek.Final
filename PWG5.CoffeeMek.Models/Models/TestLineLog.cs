using System;
using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;

public class TestLineLog : MachineLog
{
    public int Id {  get; set; }
    [Required]
    public required string TestResult {  get; set; }
    [Required]
    public double BoilerPressure {  get; set; }
    [Required]
    public double BoilerTemperature {  get; set; }
    [Required]
    public double EnergyConsumption {  get; set; }
   
}

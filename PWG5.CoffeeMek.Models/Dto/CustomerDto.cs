using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;
public class CustomerDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

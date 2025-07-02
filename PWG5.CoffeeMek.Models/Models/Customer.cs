using System.ComponentModel.DataAnnotations;

namespace PWG5.CoffeeMek.Data.Models;
public class Customer
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Phone { get; set; }

    public ICollection<Lot> Lots { get; }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class AddClientToTripDTO
{
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public string Email { get; set; } = null!;
    [Required] public string Telephone { get; set; } = null!;
    [Required] public string Pesel { get; set; } = null!;
    [Required] public int IdTrip { get; set; }
    [Required] public string TripName { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }
}
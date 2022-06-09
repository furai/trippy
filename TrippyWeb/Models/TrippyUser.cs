using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TrippyWeb.Model;

public class TrippyUser : IdentityUser
{
    [PersonalData]
    [Required(ErrorMessage = "Name field is required.")]
    public string Name { get; set; } = String.Empty;

    [InverseProperty("Owner")]
    public List<Trip>? OfferedTrips { get; set; }
    public int TripId { get; set; }
    public List<Trip>? JoinedTrips { get; set; }
    public List<Review>? Reviews { get; set; }
}

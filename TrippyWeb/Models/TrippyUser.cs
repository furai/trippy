using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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
}

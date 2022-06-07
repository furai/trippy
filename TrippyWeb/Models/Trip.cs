using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrippyWeb.Model;

[Table("Trips")]
public class Trip
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Beginning field is required.")]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Beginning { get; set; } = String.Empty;

    [Required(ErrorMessage = "Destination field is required.")]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Destination { get; set; } = String.Empty;

    [Required(ErrorMessage = "Start date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "Free Spots field is required.")]
    [Range(1, 4, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Display(Name = "Free spots")]
    public int FreeSpots { get; set; }

    // określenie relacji OneToMany dla jeden użytkownik moze oferować kilka przejazdów
    public string OwnerId { get; set; } = null!;
    public TrippyUser Owner { get; set; } = null!;

    [Required(ErrorMessage = "Price field is required.")]
    [Precision(5, 2)]
    public decimal Price { get; set; }
    public List<string> Stops { get; set; } = new List<string>();

    [InverseProperty("UsedTrip")]
    public List<TrippyUser> Passengers { get; set; }  = new List<TrippyUser>();
}

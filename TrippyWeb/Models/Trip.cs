using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace TrippyWeb.Model;

[Table("Trip")]
public class Trip
{
    [Key]
    public int TripID { get; set; }

    [Required(ErrorMessage = "Beginning field is required.")]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Beginning { get; set; } = String.Empty;

    [Required(ErrorMessage = "Destination field is required.")]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Destination { get; set; } = String.Empty;

    [Required(ErrorMessage = "Start date is required.")]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Duration is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Value for {0} represents minutes and  must be a positive integer.")]
    [Display(Name = "Duration (m)")]
    public int DurationInMinutes { get; set; }

    [Required(ErrorMessage = "Free Spots field is required.")]
    [Range(1, 4, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Display(Name = "Free Spots")]
    public int FreeSpots { get; set; }

    [ValidateNever]
    public string OwnerID { get; set; } = null!;

    [ValidateNever]
    public TrippyUser Owner { get; set; } = null!;

    [Required(ErrorMessage = "Price field is required.")]
    [Precision(5, 2)]
    [Range(0.0, double.MaxValue, ErrorMessage = "Value for {0} must be a positive floating point number.")]
    [Display(Name = "Price in PLN")]
    public double Price { get; set; }

    [Display(Name = "Additional Stops")]
    public string? Stops { get; set; }
    public List<Message>? Messages { get; set; }
    public List<TrippyUser> Passengers { get; set; } = new List<TrippyUser>();
    public bool NonSmoking { get; set; }
    public byte[]? Map { get; set; }
}

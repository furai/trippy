using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrippyWeb.Model;

[Table("Review")]
public class Review
{
    [Key]
    public int ReviewID { get; set; }

    [Required]
    public string Name { get; set; } = String.Empty;

    [Required(ErrorMessage = "Safety rating field is required.")]
    [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public int Safety { get; set; }

    [Required(ErrorMessage = "Behaviour rating field is required.")]
    [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public int Behaviour { get; set; }

    [Required(ErrorMessage = "Punctuality rating field is required.")]
    [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public int Punctuality { get; set; }

    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;
}

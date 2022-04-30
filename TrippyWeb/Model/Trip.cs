using System.ComponentModel.DataAnnotations;

namespace TrippyWeb.Model;

public class Trip
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Destination field is required.")]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Destination { get; set; } = String.Empty;
}

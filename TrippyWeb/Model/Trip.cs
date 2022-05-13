using System.ComponentModel.DataAnnotations;

namespace TrippyWeb.Model;

public class Trip
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Destination field is required.")]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string Destination { get; set; } = String.Empty;

    [Required(ErrorMessage = "Free Spots field is required.")]
    [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Display(Name = "Free spots")]
    public int FreeSpots { get; set; }
}

/* Jako Kierowca określam warunki przejazdu: ilość osób,
pasażerowie palący/niepalący, cena, miejsce docelowe, ewentualne
przystanki */

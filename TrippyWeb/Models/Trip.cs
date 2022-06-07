using System.ComponentModel.DataAnnotations;

namespace TrippyWeb.Model;

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
    [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Display(Name = "Free spots")]
    public int FreeSpots { get; set; }

    //określenie relacji OneToMany dla jeden uytkownik moze oferować kilka przejazdów
    public string OwnerId { get; set; } = null!;

    public virtual TrippyUser Owner { get; set; } = null!;

    public bool IsActive { get; set; } = true;
}

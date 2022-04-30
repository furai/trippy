using System.ComponentModel.DataAnnotations;

namespace TrippyWeb.Model;

public class Trip
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Destination { get; set; } = String.Empty;
}

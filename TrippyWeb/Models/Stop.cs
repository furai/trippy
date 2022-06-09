using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrippyWeb.Model;

[Table("Stop")]
public class Stop
{
    [Key]
    public int StopID { get; set; }

    [Required]
    public string Name { get; set; } = String.Empty;
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;
}

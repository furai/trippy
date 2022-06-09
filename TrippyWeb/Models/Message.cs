using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrippyWeb.Model;

[Table("Message")]
public class Message
{
    [Key]
    public int MessageID { get; set; }

    [Required]
    public string Content { get; set; } = String.Empty;
    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;
}

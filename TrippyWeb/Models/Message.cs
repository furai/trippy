using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrippyWeb.Model;

[Table("Message")]
public class Message
{
    [Key]
    public int MessageID { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedDate { get; set; }
    [Required]
    public string Content { get; set; } = String.Empty;
    public int TripID { get; set; }
    public Trip Trip { get; set; } = null!;

    public string UserName{ get; set;} = String.Empty;
}

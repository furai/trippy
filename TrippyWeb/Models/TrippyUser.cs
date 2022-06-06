using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TrippyWeb.Model;

public class TrippyUser : IdentityUser
{
    [PersonalData]
    [Required(ErrorMessage = "Name field is required.")]
    public string Name { get; set; } = String.Empty;

    public virtual ICollection<Trip> Trips { get; set; }
}

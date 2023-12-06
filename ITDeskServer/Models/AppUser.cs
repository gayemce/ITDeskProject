using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITDeskServer.Models;

public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? GoogleProvideId { get; set; }

    //virtual tanımlama için örnek
    [NotMapped]
    public override bool PhoneNumberConfirmed { get; set; }

    public string GetName()
    {
        return string.Join(" ", FirstName, LastName);
    }
}

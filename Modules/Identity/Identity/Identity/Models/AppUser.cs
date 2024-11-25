using Microsoft.AspNetCore.Identity;
using Shared.DDD;

namespace Identity.Identity.Models;

public class AppUser : IdentityUser, IEntity
{


    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

}
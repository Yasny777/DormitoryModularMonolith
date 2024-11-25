using Microsoft.AspNetCore.Identity;
using Shared.DDD;

namespace Identity.Identity.Models;

public class AppUser : IdentityUser<Guid>, IEntity
{
    public AppUser(string userName) : base(userName)
    {
    }

    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

    public static AppUser Create(string userName)
    {
        return new AppUser(userName)
        {
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }
}
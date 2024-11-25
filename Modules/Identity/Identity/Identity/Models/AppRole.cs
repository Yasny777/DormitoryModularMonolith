using Microsoft.AspNetCore.Identity;

namespace Identity.Identity.Models;

public class AppRole : IdentityRole<Guid>
{
    public AppRole(string roleName) : base(roleName)
    {
    }

    public AppRole()
    {
    }
}
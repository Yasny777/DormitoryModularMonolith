using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.DDD;

namespace User.User.Modules;

public class AppUser : IdentityUser<Guid>, IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
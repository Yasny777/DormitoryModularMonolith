using Shared.DDD;

namespace Identity.Identity.Models;

public class AppUserInfo : Entity<Guid>
{
    public string IndexNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
using Shared.DDD;

namespace Identity.Identity.Models;

public class AppUserInfo : Entity<Guid>
{
    public string IndexNumber { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
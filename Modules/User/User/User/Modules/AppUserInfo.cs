using Shared.DDD;

namespace User.User.Modules;

public class AppUserInfo : Entity<Guid>
{
    public string IndexNumber { get; set; }
}
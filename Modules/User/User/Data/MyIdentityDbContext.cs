using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.User.Modules;

namespace User.Data;

public class MyIdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
    {
    }

    public DbSet<AppUserInfo> AppUserInfos => Set<AppUserInfo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("asp_identity");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.User.Modules;

namespace User.Data.Configuration;

public class AppUserInfoConfiguration : IEntityTypeConfiguration<AppUserInfo>
{
    public void Configure(EntityTypeBuilder<AppUserInfo> builder)
    {
        builder.Property(x => x.IndexNumber).HasMaxLength(50);
    }
}
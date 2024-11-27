using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Configuration;

public class AppUserInfoConfiguration : IEntityTypeConfiguration<AppUserInfo>
{
    public void Configure(EntityTypeBuilder<AppUserInfo> builder)
    {
        builder.Property(x => x.IndexNumber).HasMaxLength(50);
    }
}
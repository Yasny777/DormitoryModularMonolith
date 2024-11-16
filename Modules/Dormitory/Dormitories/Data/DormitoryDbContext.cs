using System.Reflection;
using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormitories.Data;

public class DormitoryDbContext : DbContext
{
    public DormitoryDbContext(DbContextOptions<DormitoryDbContext> options) : base(options)
    {
    }

    public DbSet<Dormitory> Dormitories => Set<Dormitory>();
    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("dormitory");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
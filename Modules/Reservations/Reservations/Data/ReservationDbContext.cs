using System.Reflection;

namespace Reservations.Data;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
    {
    }

    public DbSet<Semester> Semesters => Set<Semester>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<PriorityWindow> PriorityWindows => Set<PriorityWindow>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("reservation");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
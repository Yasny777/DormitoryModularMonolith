using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Reservations.Reservations.Models;

namespace Reservations.Data;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
    {
    }

    public DbSet<Reservation> Reservations => Set<Reservation>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("reservation");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
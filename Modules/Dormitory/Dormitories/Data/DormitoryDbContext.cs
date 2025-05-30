﻿using System.Reflection;

namespace Dormitories.Data;

public class DormitoryDbContext(DbContextOptions<DormitoryDbContext> options) : DbContext(options)
{
    public DbSet<Dormitory> Dormitories => Set<Dormitory>();
    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<RoomOccupant> RoomOccupants { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("dormitory");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}


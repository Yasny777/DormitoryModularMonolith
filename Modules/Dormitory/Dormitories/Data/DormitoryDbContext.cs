using Dormitories.Dormitories.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormitories.Data;

public class DormitoryDbContext : DbContext
{
    public DormitoryDbContext(DbContextOptions<DormitoryDbContext> options) : base(options)
    {
    }

    public DbSet<Dormitory> Dormitories => Set<Dormitory>();
}
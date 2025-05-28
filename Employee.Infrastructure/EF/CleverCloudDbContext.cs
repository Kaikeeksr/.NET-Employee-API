using Employee.Domain;
using Microsoft.EntityFrameworkCore;


namespace Employee.Infrastructure.EF;

public partial class CleverCloudDbContext : DbContext
{
    public CleverCloudDbContext(DbContextOptions<CleverCloudDbContext> options)
        : base(options) { }

    public DbSet<TblEmployees> TblEmployees { get; set; }
    public DbSet<TblStatus> TblStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleverCloudDbContext).Assembly);
    }
}

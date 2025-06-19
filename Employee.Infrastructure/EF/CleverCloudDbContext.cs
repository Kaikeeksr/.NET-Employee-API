using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Employee.Infrastructure.EF;

public partial class CleverCloudDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public CleverCloudDbContext(DbContextOptions<CleverCloudDbContext> options)
        : base(options) { }

    public DbSet<TblEmployees> TblEmployees { get; set; }
    public DbSet<TblStatus> TblStatus { get; set; }
    public DbSet<TblDepartments> TblDepartments { get; set; }
    public DbSet<TblGenders>  TblGenders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(CleverCloudDbContext).Assembly);
    }
}

public class CleverCloudDbContextFactory : IDesignTimeDbContextFactory<CleverCloudDbContext>
{
    public CleverCloudDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CleverCloudDbContext>();
        
        var connectionString = Environment.GetEnvironmentVariable("CONNECTIO_STRING");
        
        optionsBuilder.UseMySQL(connectionString);
        
        return new CleverCloudDbContext(optionsBuilder.Options);
    }
}
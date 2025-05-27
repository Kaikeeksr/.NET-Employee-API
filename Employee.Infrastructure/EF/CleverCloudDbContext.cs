using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Employee.Infrastructure.EF;
public partial class CleverCloudDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public CleverCloudDbContext()
    {
        _configuration = new ConfigurationLoader().Configuration;
    }

    public CleverCloudDbContext(DbContextOptions<CleverCloudDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblEmployees> TblEmployees { get; set; }

    public virtual DbSet<TblStatus> TblStatus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("ConnectionStrings:CleverCloud");
        optionsBuilder.UseMySQL(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblEmployees>(entity =>
        {
            entity.HasKey(e => e.EId).HasName("PRIMARY");

            entity.ToTable("tbl_employees");

            entity.HasIndex(e => e.ECpf, "e_cpf").IsUnique();

            entity.HasIndex(e => e.EEmail, "e_email").IsUnique();

            entity.HasIndex(e => e.EStatus, "fk_e_status");

            entity.Property(e => e.EId).HasColumnName("e_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ECpf)
                .HasMaxLength(11)
                .HasColumnName("e_cpf");
            entity.Property(e => e.EDepartament)
                .HasColumnType("enum('Design','Suporte','Marketing','TI','Jurídico','Logística')")
                .HasColumnName("e_departament");
            entity.Property(e => e.EEmail).HasColumnName("e_email");
            entity.Property(e => e.EGender)
                .HasMaxLength(12)
                .HasColumnName("e_gender");
            entity.Property(e => e.EName)
                .HasMaxLength(255)
                .HasColumnName("e_name");
            entity.Property(e => e.EOrigem)
                .HasMaxLength(15)
                .HasColumnName("e_origem");
            entity.Property(e => e.EStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("e_status");
            entity.Property(e => e.ETel)
                .HasMaxLength(12)
                .HasColumnName("e_tel");
            entity.Property(e => e.EWage)
                .HasMaxLength(12)
                .HasColumnName("e_wage");

            entity.HasOne(d => d.EStatusNavigation).WithMany(p => p.TblEmployees)
                .HasPrincipalKey(p => p.EStatus)
                .HasForeignKey(d => d.EStatus)
                .HasConstraintName("fk_e_status");
        });

        modelBuilder.Entity<TblStatus>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("tbl_status");

            entity.HasIndex(e => e.EStatus, "e_status").IsUnique();

            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.EStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("e_status");
            entity.Property(e => e.StatusDescr)
                .HasMaxLength(35)
                .HasColumnName("status_descr");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

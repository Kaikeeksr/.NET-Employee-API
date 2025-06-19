using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Infrastructure.EF.Mapping;

public class TblEmployeesMapping : IEntityTypeConfiguration<TblEmployees>
{
    public void Configure(EntityTypeBuilder<TblEmployees> builder)
    {
        builder.ToTable("tbl_employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Cpf)
            .HasColumnName("cpf")
            .HasMaxLength(11)
            .IsRequired();

        builder.HasIndex(e => e.Cpf)
            .IsUnique();

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.Property(e => e.Telephone)
            .HasColumnName("telephone")
            .HasMaxLength(12);

        builder.Property(e => e.Wage)
            .HasColumnName("wage")
            .HasMaxLength(12);

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(1);

        builder.Property(e => e.Source)
            .HasColumnName("source")
            .HasMaxLength(15);

        builder.Property(e => e.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired();
        
        builder.Property(e => e.GenderId)
            .HasColumnName("gender_id")
            .IsRequired();

        builder.HasOne(e => e.StatusNavigation)
            .WithMany()
            .HasForeignKey(e => e.Status)
            .HasConstraintName("fk_status");
        
        builder.HasOne(e => e.DepartmentNavigation)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.GenderNavigation)
            .WithMany(e => e.Employees)
            .HasForeignKey(e => e.GenderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Infrastructure.EF.Mapping
{
    public class TblEmployeesMapping : IEntityTypeConfiguration<TblEmployees>
    {
        public void Configure(EntityTypeBuilder<TblEmployees> builder)
        {
            builder.ToTable("tbl_employees");

            builder.HasKey(e => e.EId);

            builder.Property(e => e.EId)
                .HasColumnName("e_id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EName)
                .HasColumnName("e_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.ECpf)
                .HasColumnName("e_cpf")
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(e => e.ECpf)
                .IsUnique();

            builder.Property(e => e.EEmail)
                .HasColumnName("e_email")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(e => e.EEmail)
                .IsUnique();

            builder.Property(e => e.ETel)
                .HasColumnName("e_tel")
                .HasMaxLength(12);

            builder.Property(e => e.EGender)
                .HasColumnName("e_gender")
                .HasMaxLength(12);

            builder.Property(e => e.EWage)
                .HasColumnName("e_wage")
                .HasMaxLength(12);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
            
            builder.Property(e => e.EStatus)
                .HasColumnName("e_status")
                .HasMaxLength(1);

            builder.Property(e => e.ESource)
                .HasColumnName("e_source")
                .HasMaxLength(15);

            builder.Property(e => e.DepartmentId)
                .HasColumnName("department_id")
                .IsRequired();

            builder.HasOne(e => e.EStatusNavigation)
                .WithMany()
                .HasForeignKey(e => e.EStatus)
                .HasConstraintName("fk_e_status");
            
            builder.HasOne(e => e.EDepartmentNavigation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

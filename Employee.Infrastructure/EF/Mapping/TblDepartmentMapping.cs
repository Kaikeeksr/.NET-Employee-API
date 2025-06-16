using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Infrastructure.EF.Mapping;

public class TblDepartmentMapping : IEntityTypeConfiguration<TblDepartments>
{
    public void Configure(EntityTypeBuilder<TblDepartments> builder)
    {
        builder.ToTable("tbl_departments");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Department)
            .HasColumnName("department")
            .IsRequired()
            .HasMaxLength(50);
        
    }
}
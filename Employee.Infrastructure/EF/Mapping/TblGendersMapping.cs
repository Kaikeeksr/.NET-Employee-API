using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Infrastructure.EF.Mapping;

public class TblGendersMapping : IEntityTypeConfiguration<TblGenders>
{
    public void Configure(EntityTypeBuilder<TblGenders> builder)
    {
        builder.ToTable("tbl_genders");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Gender)
            .HasColumnName("gender")
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
    }
}
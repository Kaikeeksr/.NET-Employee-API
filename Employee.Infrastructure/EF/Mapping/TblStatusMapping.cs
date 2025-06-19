using Employee.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Infrastructure.EF.Mapping
{
    public class TblStatusMapping : IEntityTypeConfiguration<TblStatus>
    {
        public void Configure(EntityTypeBuilder<TblStatus> builder)
        {
            builder.ToTable("tbl_status");

            builder.HasKey(s => s.IdStatus);

            builder.Property(s => s.IdStatus)
                .HasColumnName("id_status")
                .IsRequired()   
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Status)
                .HasColumnName("e_status")
                .HasMaxLength(1)
                .IsRequired();

            builder.HasIndex(s => s.Status)
                .IsUnique();

            builder.Property(s => s.StatusDescription)
                .HasColumnName("status_descr")
                .HasMaxLength(35);

            builder.HasMany(s => s.TblEmployees)
                .WithOne(e => e.StatusNavigation)
                .HasForeignKey(e => e.Status)
                .HasPrincipalKey(s => s.Status)
                .HasConstraintName("fk_e_status");
        }
    }
}

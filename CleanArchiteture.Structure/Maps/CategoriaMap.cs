using CleanArchiteture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchiteture.Structure.Maps
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIA");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.CodCategoria)
                .HasColumnName("COD_CATEGORIA")
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.DescCategoria)
                .HasColumnName("DESC_CATEGORIA")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.Produtos)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId);
        }
    }
}

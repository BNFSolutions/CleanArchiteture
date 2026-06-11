using CleanArchiteture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchiteture.Structure.Maps
{
    public class CompraMap : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("COMPRA");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.ClienteId)
                .HasColumnName("ID_CLIENTE")
                .IsRequired();

            builder.Property(x => x.DataCompra)
                .HasColumnName("DATA_COMPRA")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Compras)
                .HasForeignKey(x => x.ClienteId);

            builder.HasMany(x => x.Itens)
                .WithOne(x => x.Compra)
                .HasForeignKey(x => x.CompraId);
        }
    }
}

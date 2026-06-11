using CleanArchiteture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchiteture.Structure.Maps
{
    public class CompraItemMap : IEntityTypeConfiguration<CompraItem>
    {
        public void Configure(EntityTypeBuilder<CompraItem> builder)
        {
            builder.ToTable("COMPRA_ITEM");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.CompraId)
                .HasColumnName("ID_COMPRA")
                .IsRequired();

            builder.Property(x => x.ProdutoId)
                .HasColumnName("ID_PRODUTO")
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .HasColumnName("QUANTIDADE")
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(x => x.Compra)
                .WithMany(x => x.Itens)
                .HasForeignKey(x => x.CompraId);

            builder.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId);
        }
    }
}

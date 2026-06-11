using CleanArchiteture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchiteture.Structure.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            builder.ToTable("PRODUTOS");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.CodProduto)
                .HasColumnName("COD_PRODUTO")
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.DescProduto)
                .HasColumnName("DESC_PRODUTO")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.VlrPreco)
                .HasColumnName("VLR_PRECO")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .HasColumnName("DATA_CADASTRO")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.CategoriaId)
                .HasColumnName("ID_CATEGORIA")
                .IsRequired();
        }
    }
}

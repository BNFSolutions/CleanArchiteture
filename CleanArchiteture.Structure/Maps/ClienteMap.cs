using CleanArchiteture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchiteture.Structure.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.Cpf)
                .HasColumnName("CPF")
                .HasColumnType("nvarchar(11)")
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.Compras)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId);
        }
    }
}

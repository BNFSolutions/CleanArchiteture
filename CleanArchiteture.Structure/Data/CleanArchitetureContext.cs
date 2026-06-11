using CleanArchiteture.Domain.Models;
using CleanArchiteture.Structure.Maps;
using Microsoft.EntityFrameworkCore;

namespace CleanArchiteture.Structure.Data
{
    public class CleanArchitetureContext : DbContext
    {
        public CleanArchitetureContext(DbContextOptions<CleanArchitetureContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraItem> CompraItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new CompraMap());
            modelBuilder.ApplyConfiguration(new CompraItemMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

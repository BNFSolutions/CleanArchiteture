using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;
using CleanArchiteture.Structure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchiteture.Structure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CleanArchitetureContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();
            services.AddScoped<ICompraItemRepository, CompraItemRepository>();

            return services;
        }
    }
}

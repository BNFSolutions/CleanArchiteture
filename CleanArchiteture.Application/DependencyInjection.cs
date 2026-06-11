using CleanArchiteture.Application.Parsing;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Application.Services;
using CleanArchiteture.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchiteture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICategoriaService, CategoriaService>();
            services.AddSingleton<IProdutoService, ProdutoService>();
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<ICompraService, CompraService>();
            services.AddSingleton<ICompraItemService, CompraItemService>();

            services.AddSingleton<ICategoriaParse, CategoriaParse>();
            services.AddSingleton<IProdutoParse, ProdutoParse>();
            services.AddSingleton<IClienteParse, ClienteParse>();
            services.AddSingleton<ICompraParse, CompraParse>();
            services.AddSingleton<ICompraItemParse, CompraItemParse>();

            return services;
        }
    }
}

using CleanArchiteture.Application;
using CleanArchiteture.Structure.IoC;

namespace CleanArchitetureAPI.Configuration
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não foi configurada.");

            services.AddInfrastructure(connectionString);
            services.AddApplication();

            return services;
        }
    }
}

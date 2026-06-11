namespace CleanArchitetureAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}

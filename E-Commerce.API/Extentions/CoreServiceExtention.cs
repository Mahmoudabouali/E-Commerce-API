using Services.Abstraction;
using Services;

namespace E_Commerce.API.Extentions
{
    public static class CoreServiceExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);

            return services;
        }
    }
}

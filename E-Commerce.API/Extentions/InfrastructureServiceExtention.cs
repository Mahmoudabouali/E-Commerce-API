using Services.Abstraction;
using Services;
using Domain.Contracts;
using Persistence.Data;
using Persistence.Repositories;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extentions
{
    public static class InfrastructureServiceExtention
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<StoreContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultSQLConnection"));
            });

            return services;
        }
    }
}

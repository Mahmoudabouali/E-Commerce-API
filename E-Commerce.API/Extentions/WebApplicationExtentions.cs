using Domain.Contracts;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extentions
{
    public static class WebApplicationExtentions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            await dbInitialize.InitializeAsync();
            await dbInitialize.InitializeIdentityAsync();
            return app;
        }
        public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}

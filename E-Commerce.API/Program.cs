
using Domain.Contracts;
using E_Commerce.API.Extentions;
using E_Commerce.API.Factories;
using E_Commerce.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.Abstraction;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container. 

            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            

            var app = builder.Build();

            app.UseCustomExceptionMiddleware();

            await app.SeedDbAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.UseStaticFiles();

            app.Run();

            //async Task InitializeDbAsync(WebApplication app)
            //{
            //    // create object from type that implement IDbInitialze
            //    using var scope = app.Services.CreateScope();
            //    var dbInitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            //    await dbInitialize.InitializeAsync();
            //}
        }
    }
}

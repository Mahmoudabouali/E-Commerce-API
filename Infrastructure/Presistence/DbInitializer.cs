using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task InitializeAsync()
        {
            // create database if it doesn't exist & apply any pending migration
            if ((await _storeContext.Database.GetPendingMigrationsAsync()).Any())
                await _storeContext.Database.MigrateAsync();


            // apply data seeding
            if (!_storeContext.ProductTypes.Any())
            {
                // read types from file as string
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");

                // transform into C# objects
                var Types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if(Types is not null && Types.Any())
                {
                    await _storeContext.ProductTypes.AddRangeAsync(Types);
                    await _storeContext.SaveChangesAsync();
                }
            }
            if (!_storeContext.ProductBrands.Any())
            {
                // read types from file as string
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");

                // transform into C# objects
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (Brands is not null && Brands.Any())
                {
                    await _storeContext.ProductBrands.AddRangeAsync(Brands);
                    await _storeContext.SaveChangesAsync();
                }
            }
            if (!_storeContext.Products.Any())
            {
                // read types from file as string
                var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");

                // transform into C# objects
                var Products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (Products is not null && Products.Any())
                {
                    await _storeContext.Products.AddRangeAsync(Products);
                    await _storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
// \Infrastructure\Presistence\Data\Seeding\types.json`
// \Infrastructure\Presistence\Data\Seeding\brands.json
// \Infrastructure\Presistence\Data\Seeding\products.json
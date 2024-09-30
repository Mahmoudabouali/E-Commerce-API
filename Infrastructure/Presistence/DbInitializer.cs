using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                //create db if it doesn't exist & appling any pending migration
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();

                if (!_storeContext.ProductTypes.Any())
                {
                    // read types from file as string
                    var typeData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");
                    // tranfare into c# objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    // add db & saveChanges
                    if (types is null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                if (!_storeContext.ProductBrands.Any())
                {
                    // read types from file as string
                    var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");
                    // tranfare into c# objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    // add db & saveChanges
                    if (brands is null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                if (!_storeContext.Products.Any())
                {
                    // read types from file as string
                    var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");
                    // tranfare into c# objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    // add db & saveChanges
                    if (products is null && products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(products);
                        await _storeContext.SaveChangesAsync();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


//..\Infrastructure\Presistence\Data\Seeding\
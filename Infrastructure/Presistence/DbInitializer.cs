using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreContext storeContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task InitializeIdentityAsync()
        {
            // seed default roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            // seed default users
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "super Admin User",
                    Email = "superAdminUser@gmail.com",
                    UserName = "superAdminUser",
                    PhoneNumber = "123456789",
                };
                var adminUser = new User
                {
                    DisplayName = "admin User",
                    Email = "adminUser@gmail.com",
                    UserName = "adminUser",
                    PhoneNumber = "123456789",
                };

                await _userManager.CreateAsync(superAdminUser, "Passw0rd");
                await _userManager.CreateAsync(adminUser, "Passw0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

        }
    }
}
// \Infrastructure\Presistence\Data\Seeding\types.json`
// \Infrastructure\Presistence\Data\Seeding\brands.json
// \Infrastructure\Presistence\Data\Seeding\products.json
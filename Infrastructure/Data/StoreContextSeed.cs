using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.VisualBasic;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        //could be used without initialization class
        public static async Task SeedAsync(StoreContext context)
        {
            // Implementation for seeding the database goes here.
            // This could involve checking if certain tables are empty
            // and populating them with initial data.
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                //convert stream data to list of products
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                
                if (products == null) return;

                context.Products.AddRange(products);//exception has been handled above
                await context.SaveChangesAsync();
            }
        }
    }
}
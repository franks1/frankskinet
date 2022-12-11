using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedData(StoreContext context, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            try
            {
                if (!context.ProductBrands.Any())
                {
                    logger.LogInformation("Migrating brands");
                    var brandsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    context.ProductBrands.AddRange(brands);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Brands successfully migrated");
                }

                if (!context.ProductTypes.Any())
                {
                    logger.LogInformation("Migrating product types");
                    var typesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    context.ProductTypes.AddRange(types);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Product types successfully migrated");
                }

                if (!context.Product.Any())
                {
                    logger.LogInformation("Migrating products");
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    context.Product.AddRange(products);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Products successfully migrated");
                }
                
                if (!context.DeliveryMethods.Any())
                {
                    logger.LogInformation("Migrating product delivery methods");
                    var deliveryData = await File.ReadAllTextAsync("../Infrastructure/SeedData/delivery.json");
                    var data = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                    if (data is not null)
                    {
                        context.DeliveryMethods.AddRange(data);
                        await context.SaveChangesAsync();
                        logger.LogInformation("Product delivery method successfully migrated");
                    }
                }
                
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        }
    }
}
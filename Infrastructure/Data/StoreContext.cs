
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<DeliveryMethod> DeliveryMethods => Set<DeliveryMethod>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Product => Set<Product>();
        public DbSet<ProductBrand> ProductBrands => Set<ProductBrand>();
        public DbSet<ProductType> ProductTypes => Set<ProductType>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(f => f.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.
                        GetProperties().
                        Where(f => f.PropertyType == typeof(DateTimeOffset));
                    foreach (var property in properties)
                    {                        
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                    foreach (var property in dateTimeProperties)
                    {                        
                        builder.Entity(entityType.Name).Property(property.Name).
                            HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                    
                }
            }
        }
    }
}
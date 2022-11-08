
using Microsoft.EntityFrameworkCore;
using  Core.Entities;
namespace Infrastructure.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options){}

        public DbSet<Product> Product => Set<Product>();
        protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
        
        
    }
}
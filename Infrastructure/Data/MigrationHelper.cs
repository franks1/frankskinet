using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public static class MigrationHelper
    {
        public static async Task RunMigration(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILoggerFactory>();
                var log = logger.CreateLogger("Test");
                var context = service.GetRequiredService<StoreContext>();

                try
                {
                    log.LogInformation("Initializing migration");
                    await context.Database.MigrateAsync();
                    log.LogInformation("Migration successfully completed");
                    await StoreContextSeed.SeedData(context, logger);

                    var userManager = service.GetRequiredService<UserManager<AppUser>>();
                    var identityDbContext = service.GetRequiredService<AppIdentityDbContext>();
                    await AppIdentitySeed.SeedIdentityUsers(userManager, log);
                }
                catch (System.Exception e)
                {
                    log.LogError(e, "An error occurred during migration");
                }
            }
        }
    }
}
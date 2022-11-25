using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
namespace Infrastructure.Identity;

public class AppIdentitySeed
{
    public static async Task SeedIdentityUsers(UserManager<AppUser> userManager, ILogger log)
    {
        if (!userManager.Users.Any())
        {
            log.LogInformation("Attempting to create user");

            var user = new AppUser
            {
                DisplayName = "Frank",
                Email = "info@default.com",
                UserName = "Frank",
                Address = new Address()
                {
                    FirstName = "Frank",LastName = "Banini",City = "Accra",
                    State = "A",Street = "GB", ZipCode = "233"
                }
            };
            await userManager.CreateAsync(user, "Password@1");
            log.LogInformation("User creation successfully completed");
        }
    }
}
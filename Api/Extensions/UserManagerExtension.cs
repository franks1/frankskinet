using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class UserManagerExtension
{
    public static Task<AppUser> FindUserWithAddress(this UserManager<AppUser> userManager,
        ClaimsPrincipal claimsPrincipal)
    {
        var userEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        var userInfo = userManager.Users.Include(a => a.Address)
            .FirstOrDefaultAsync(a => a.Email == userEmail);
        return userInfo;
    }

    public static Task<AppUser> FindUserFromClaimsPrincipal(this UserManager<AppUser> userManager,
        ClaimsPrincipal claimsPrincipal)
    {
        var userEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        var userInfo = userManager.Users
            .FirstOrDefaultAsync(a => a.Email == userEmail);
        return userInfo;
    }
}
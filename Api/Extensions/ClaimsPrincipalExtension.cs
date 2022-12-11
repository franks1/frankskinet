using System.Security.Claims;

namespace Api.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string RetrieveEmailFromClaims(this ClaimsPrincipal user)
    {
     return  user?.Claims?.
         FirstOrDefault(a=>a.Type==ClaimTypes.Email)?.Value;
     
     //or
    //return user?.FindFirstValue(ClaimTypes.Email);
     
    }
}
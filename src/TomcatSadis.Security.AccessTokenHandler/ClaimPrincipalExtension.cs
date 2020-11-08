using System.Security.Claims;

namespace TomcatSadis.Security.AccessTokenHandler
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            return claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetGivenName(this ClaimsPrincipal claims)
        {
            return claims?.FindFirst(ClaimTypes.GivenName)?.Value;
        }

        public static string GetFamilyName(this ClaimsPrincipal claims)
        {
            return claims?.FindFirst(ClaimTypes.Surname)?.Value;
        }

        public static string GetName(this ClaimsPrincipal claims)
        {
            return claims?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}

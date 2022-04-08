using System.Security.Claims;
using static VideoGamesShop.Core.Constants.UserConstants;

namespace VideoGamesShop.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(Roles.Administrator);

        public static bool IsDeveloper(this ClaimsPrincipal user)
            => user.IsInRole(Roles.Developer);
    }
}

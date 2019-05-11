using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web
{
    public static class UserExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static string GetAvatarUrl(this ClaimsPrincipal principal)
        {
            var avatar = principal.FindFirst("Avatar").Value;
            return avatar;//.ToImageUrl();
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name).Value;
        }

        public static string GetUserDisplayName(this ClaimsPrincipal principal)
        {
           
            return principal.FindFirst(ClaimTypes.GivenName).Value;
        }

        public static bool IsInRoles(this ClaimsPrincipal principal, List<string> roles)
        {

            foreach(var role in roles)
            {
                if (principal.IsInRole(role))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

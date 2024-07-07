using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Authorized
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasAnyRole(this ClaimsPrincipal user, params string[] roles)
        {
            foreach (var role in roles)
            {
                if (user.HasClaim(ClaimTypes.Role, role))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

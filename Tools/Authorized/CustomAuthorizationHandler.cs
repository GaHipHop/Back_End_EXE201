using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Authorized
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>, IAuthorizationFilter
    {
        private readonly string[] _requiredRoles;

        public CustomAuthorizationHandler(params string[] requiredRoles)
        {
            _requiredRoles = requiredRoles;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (context.User.HasAnyRole(requirement.RequiredRoles))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new CustomException.ForbbidenException("You don't have permission to access this resource.");
                }
            }
            else
            {
                throw new CustomException.ForbbidenException("User is not authenticated.");
            }

            return Task.CompletedTask;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var requirement = new CustomAuthorizationRequirement(_requiredRoles);
            HandleRequirementAsync(new AuthorizationHandlerContext((IEnumerable<IAuthorizationRequirement>)(new[] { this }), context.HttpContext.User, requirement), requirement).Wait();
        }
    }
}

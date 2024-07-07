using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Authorized
{
    public class CustomAuthorizationRequirement : IAuthorizationRequirement
    {
        public string[] RequiredRoles { get; }

        public CustomAuthorizationRequirement(params string[] requiredRoles)
        {
            RequiredRoles = requiredRoles;
        }
    }
}

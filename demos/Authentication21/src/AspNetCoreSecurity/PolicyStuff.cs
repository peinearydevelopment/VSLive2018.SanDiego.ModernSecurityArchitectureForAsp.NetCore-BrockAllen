using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreSecurity
{
    public class JobLevelRequirement : IAuthorizationRequirement
    {
        public int Level { get; set;  }
    }

    public class JobLevelRequirementHandler : AuthorizationHandler<JobLevelRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            JobLevelRequirement requirement)
        {
            var user = context.User;
            var level = requirement.Level;

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

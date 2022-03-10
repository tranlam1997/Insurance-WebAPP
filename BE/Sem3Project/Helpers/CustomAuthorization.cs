using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Sem3Project.Helpers
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(CustomAuthorization))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }


    public class CustomAuthorization : IAuthorizationFilter
    {
        readonly Claim _claim;

        public CustomAuthorization(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                //context.Result = new UnauthorizedResult();
                //return;

                context.Result = new ObjectResult(
                new
                {
                    message = "Unauthorized"
                })
                { StatusCode = 401 };
                return;
            }

            List<string> roles = _claim.Value.Split(',').ToList();
            var hasClaim = false;

            foreach (string role in roles)
            {
                if (context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == role))
                {
                    hasClaim = true;
                    return;
                }
            }

            if (!hasClaim)
            {
                context.Result = new ObjectResult(
                new
                {
                    message = $"You need {string.Join(" or ", roles)} role"
                })
                { StatusCode = 403 };
                return;
            }
        }
    }
}

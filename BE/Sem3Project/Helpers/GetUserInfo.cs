using Microsoft.AspNetCore.Mvc;
using Sem3Project.Models;
using System.Linq;
using System.Security.Claims;

namespace Sem3Project.Helpers
{
    public class GetUserInfo: ControllerBase
    {
        public GetUserInfo()
        {
            
        }

        public Identifier GetABC()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Identifier
                {
                    Id = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }
            return null;
        }
    }
}

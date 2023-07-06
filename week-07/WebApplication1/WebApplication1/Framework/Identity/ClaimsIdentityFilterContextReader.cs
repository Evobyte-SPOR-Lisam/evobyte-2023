using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApplication1.Framework.Identity
{
    public interface IClaimsIdentityFilterContextReader
    {
        ClaimsIdentityFilterContext ReadContext(AuthorizationFilterContext context);
    }

    public class ClaimsIdentityFilterContext
    {
        public bool IsAuthenticated { get; set; }

        public bool IsAuthorized { get; set; }

        public string NameIdentifier { get; set; }

        public string Sid { get; set; }

        public string OriginatorID { get; set; }

        public bool SkipSidCheck { get; set; }
    }

    public class ClaimsIdentityFilterContextReader : IClaimsIdentityFilterContextReader
    {
        public ClaimsIdentityFilterContext ReadContext(AuthorizationFilterContext context)
        {
            ClaimsIdentityFilterContext claimsIdentityFilterContext = new ClaimsIdentityFilterContext() { IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated };

            if (claimsIdentityFilterContext.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

                claimsIdentityFilterContext.NameIdentifier = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                claimsIdentityFilterContext.OriginatorID = claimsIdentity.Claims.LastOrDefault(x => x.Type == ClaimTypes.Actor)?.Value;
                claimsIdentityFilterContext.Sid = claimsIdentity.Claims.LastOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;

                string[] userRoles = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
                string[] requiredRoles = context.ActionDescriptor.FilterDescriptors.Where(x => x.Filter is RequireRoleAttribute).Select(x => x.Filter as RequireRoleAttribute).Select(x => x.Role).ToArray();

                claimsIdentityFilterContext.IsAuthorized = requiredRoles.Length == 0 || userRoles.Length == 0 || requiredRoles.Any(requiredRole => userRoles.Contains(requiredRole));
            }

            return claimsIdentityFilterContext;
        }
    }
}
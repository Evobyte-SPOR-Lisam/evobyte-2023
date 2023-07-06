using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Framework.Identity
{
    public class IdentityAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IClaimsIdentityFilterContextReader claimsIdentityFilterContextReader;

        public IdentityAuthorizationFilter(
            IClaimsIdentityFilterContextReader claimsIdentityFilterContextReader)
        {
            this.claimsIdentityFilterContextReader = claimsIdentityFilterContextReader;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ClaimsIdentityFilterContext claimsIdentityFilterContext = claimsIdentityFilterContextReader.ReadContext(context);

            if (claimsIdentityFilterContext.IsAuthenticated)
            {
                if (claimsIdentityFilterContext.IsAuthorized)
                {
                    Identity identity = BuildIdentity(claimsIdentityFilterContext);

                    context.HttpContext.AddIdentity(identity);
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

        private Identity BuildIdentity(ClaimsIdentityFilterContext claimsIdentityFilterContext)
        {
            return new Identity()
            {
                ID = Guid.Parse(claimsIdentityFilterContext.NameIdentifier),
                OriginatorID = string.IsNullOrWhiteSpace(claimsIdentityFilterContext.OriginatorID) ? null : Guid.Parse(claimsIdentityFilterContext.OriginatorID)
            };
        }
    }
}
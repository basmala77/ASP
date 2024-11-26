using CRUD_Operations.Controllers;
using CRUD_Operations.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CRUD_Operations.Authorization
{
    public class PermassionBasedAuthorization(ApplicationDbContext _context) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var attribute = (CheckPermassionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermassionAttribute);

            if (attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                    return;
                }
                else {
                    var userIdClaim = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var hasPermission = _context.Set<UserPermissions>().Any(x => x.UserId == userIdClaim && x.PermissionId == attribute.Permission);
                    if (!hasPermission)
                    {
                        context.Result = new ForbidResult();
                    }
                }
                
            }
        }
    }
}

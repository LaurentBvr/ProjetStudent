using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetEtudiantBackend.Entity;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace StudentBackend.Annotation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorize : Attribute, IAuthorizationFilter
    {
        public Authorization[] AuthorizedRoles { get; set; }

        public Authorize(params Authorization[] roles)
        {
            AuthorizedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var validator = context.HttpContext.RequestServices.GetRequiredService<JwtValidator>();
            var claimsPrincipal = validator.ValidateJwtToken(token);

            if (claimsPrincipal == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userRole = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
            if (userRole == null || !AuthorizedRoles.Any(role => role.ToString().Equals(userRole, StringComparison.OrdinalIgnoreCase)))
            {
                context.Result = new ForbidResult();
                return;
            }

            // Set the user identity
            context.HttpContext.User = claimsPrincipal;
        }
    }

    public enum Authorization
    {
        Admin = 100,
        Instructor = 200,
        Student = 300
    }
}

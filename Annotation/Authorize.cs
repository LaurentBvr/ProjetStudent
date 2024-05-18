using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using System.Security.Claims;

namespace StudentBackend.Annotation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Authorize : Attribute, IAuthorizationFilter
    {
        public Authorization[] AuthorizedRoles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.RouteData.Values["token"]?.ToString();
            if (token == null)
            {
                throw new UnauthorizedAccessException("Aucun token fourni");
            }
            var validator = new JwtValidator();
            var claims = validator.ValidateJwtToken(token);

            if (claims is null)
            {
                throw new UnauthorizedAccessException("Pas autorisé: JWT token invalide.");
            }
            var personId = claims.FindFirst(claim => claim.Type == "PersonId")!.Value;
            /*
            
            if (personId == Context.Find(personId)) { }
            */

        }
    }
    public enum Authorization
    {
        Admin,
        Instructor,
        Student
    }
}


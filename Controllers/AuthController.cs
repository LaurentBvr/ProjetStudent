using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using ProjetEtudiantBackend.Entity;
using StudentBackend.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string _secret;
        private readonly StudentProjetContext _ctx;


        public AuthController(IConfiguration configuration, StudentProjetContext ctx)
        {
            _secret = configuration.GetValue<string>("Jwt:Secret");
            _ctx = ctx;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginrequest)
        {
            // Ici vous devriez vérifier les informations d'identification de l'utilisateur (par exemple, vérifier le mot de passe hashé dans la base de données)
            if (IsValidUser(loginrequest))
            {
                var token = GenerateJwtToken(loginrequest.Email);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private bool IsValidUser(LoginRequest loginRequest)
        {
            var person = _ctx.People.SingleOrDefault(u => u.Email == loginRequest.Email);
            if(person == null) { return false; }
            return BCrypt.Net.BCrypt.Verify(loginRequest.Password, person.Password);
            
        }

        private string GenerateJwtToken(string email)
        {
            var person = _ctx.People.SingleOrDefault(u => u.Email == email);
            if (person == null) { throw new Exception("User not found"); }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("firstName", person.FirstName),
                    new Claim("lastName", person.LastName),
                    new Claim(ClaimTypes.Role, person.Role.ToString()),
                    new Claim("sId", person.PersonId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            // Si l'utilisateur est authentifié, le token est valide
            var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            // Optionnel: vous pouvez ajouter des informations supplémentaires sur le token
            var exp = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;

            return Ok(new { Message = "Token is valid", Expiration = exp });
        }


    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class JwtValidator
{
    private string _secretKey = "Tralala"; // Your secret key used for token validation



    public ClaimsPrincipal ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        try
        {
            // Validate the token
            SecurityToken validatedToken;
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            // Check if the token is a JwtSecurityToken
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                // You can access token properties here
                var claims = jwtSecurityToken.Claims;
                // Do something with claims if needed
            }

            return claimsPrincipal;
        }
        catch (SecurityTokenException ex)
        {
            return null;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = false, // Set to true if you want to validate the issuer
            ValidateAudience = false, // Set to true if you want to validate the audience
            ValidateLifetime = true, // Check if the token is expired
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_secretKey))
        };
    }
}


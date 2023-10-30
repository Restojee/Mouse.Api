using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Mouse.NET.Users.Common;

namespace Mouse.Stick.Controllers.Auth;

public class JwtUtils
{

    static public string GenerateJwtToken(Dictionary<string, object> claims)
    {
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding
            .ASCII
            .GetBytes("this is my custom Secret key for authnetication");
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = claims,
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, claims[UserDetails.Id].ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Identity.Models;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Identity.Services.TokenService;

public interface ITokenService
{
    string GenerateJwtToken(UserSession userSession);
    string GenerateRefreshToken();
}


public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateJwtToken(UserSession userSession)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
            new Claim(ClaimTypes.Name, userSession.UserName),
            new Claim(ClaimTypes.Email, userSession.Email),
        };

        var roles = userSession.Roles;
        foreach (var roleName in roles.Select(role => role.Name))
        {
            claims.Append(new Claim(ClaimTypes.Role, roleName));
        }

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}



public record UserSession(Guid Id, string UserName, string Email, List<AppRole> Roles)
{
}
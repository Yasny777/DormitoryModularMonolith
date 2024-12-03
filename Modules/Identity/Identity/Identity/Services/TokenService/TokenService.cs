using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Identity.Features.Login.Handler;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Identity.Services.TokenService;

public interface ITokenService
{
    JwtRefreshTokensWithExpiry GenerateAccessAndRefreshToken(UserSession userSession);
}


public class TokenService(IConfiguration configuration) : ITokenService
{
    public JwtRefreshTokensWithExpiry GenerateAccessAndRefreshToken(UserSession userSession)
    {
        var token = GenerateJwtToken(userSession);
        var refreshToken = GenerateRefreshToken();
        var expiryRefreshTokenTime = DateTime.UtcNow.AddDays(1);
        return new JwtRefreshTokensWithExpiry(token.Token, token.ExpiryTime, refreshToken, expiryRefreshTokenTime);
    }
    private TokenWithExpiry GenerateJwtToken(UserSession userSession)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
            new Claim(ClaimTypes.Name, userSession.UserName),
            new Claim(ClaimTypes.Email, userSession.Email),
        };

        var roles = userSession.Roles;
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwtExpiryTime = DateTime.Now.AddMinutes(99999); //todo change

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: jwtExpiryTime,
            signingCredentials: credentials
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new TokenWithExpiry(jwtToken, jwtExpiryTime);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }



}

public record TokenWithExpiry(string Token, DateTime ExpiryTime);

public record UserSession(Guid Id, string UserName, string Email, List<string> Roles)
{
}
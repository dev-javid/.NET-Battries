using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Auth.Commands;
using $safeprojectname$.DependencyRegistration;
using Microsoft.IdentityModel.Tokens;

namespace $safeprojectname$.Services;

public class JwtService(JwtConfiguration configuration, IIdentityService identityService) : IJwtService
{
    public async Task<JwtTokens> GenerateTokenAsync(ClaimsPrincipal claimsPrincipal)
    {
        var email = Email.From(claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Email).Value);
        var user = await identityService.GetUserAsync(email);
        if (user is null)
        {
            throw CustomException.WithBadRequest("No user details found");
        }

        var refreshToken = await identityService.GenerateRefreshTokenAsync(user);
        var accessToken = GenerateAccessToken(claimsPrincipal);
        return new JwtTokens(accessToken, refreshToken);
    }

    public Email ExtractEmailFromToken(string accessToken)
    {
        var principal = GetPrincipalFromAccessToken(accessToken);
        var emailString = principal.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        return Email.From(emailString);
    }

    private ClaimsPrincipal GetPrincipalFromAccessToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(configuration.SecretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private string GenerateAccessToken(ClaimsPrincipal claimsPrincipal)
    {
        var claims = claimsPrincipal.Claims.ToList();
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.OtpLifeTimeInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration.Audience,
            Issuer = configuration.Issuer,
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}

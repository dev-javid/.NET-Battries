using System.Security.Claims;
using $safeprojectname$.Auth.Commands;

namespace $safeprojectname$.Common.Abstract;

public interface IJwtService
{
    Task<JwtTokens> GenerateTokenAsync(ClaimsPrincipal claimsPrincipal);

    Email ExtractEmailFromToken(string accessToken);
}

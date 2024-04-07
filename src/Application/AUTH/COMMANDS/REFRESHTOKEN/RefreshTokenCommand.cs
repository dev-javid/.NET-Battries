using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<JwtTokens>
{
    public required string RefreshToken { get; init; }

    public required string AccessToken { get; init; }

    public class Handler(IJwtService jwtService, IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<RefreshTokenCommand, JwtTokens>
    {
        public async Task<JwtTokens> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var email = jwtService.ExtractEmailFromToken(request.AccessToken);
            var user = await identityService.GetUserAsync(email);
            await identityService.SigninAsync(user!);
            return await jwtService.GenerateTokenAsync(httpContextAccessor.HttpContext.User);
        }
    }
}

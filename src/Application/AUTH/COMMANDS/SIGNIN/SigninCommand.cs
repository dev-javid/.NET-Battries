using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Auth.Commands.Signin;

public class SigninCommand : IRequest<JwtTokens>
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public class Handler(IJwtService jwtService, IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<SigninCommand, JwtTokens>
    {
        public async Task<JwtTokens> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            var email = Domain.Common.ValueObjects.Email.From(request.Email);
            var user = await identityService.GetUserAsync(email);
            await identityService.SigninAsync(user!);
            return await jwtService.GenerateTokenAsync(httpContextAccessor.HttpContext.User);

            throw CustomException.WithInternalServer("Unable to signin.");
        }
    }
}

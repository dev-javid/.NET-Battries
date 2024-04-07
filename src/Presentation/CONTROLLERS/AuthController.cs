using Application.Auth.Commands.RefreshToken;
using Application.Auth.Commands.Signin;
using Microsoft.AspNetCore.Authorization;

namespace $safeprojectname$.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiBaseController
    {
        [HttpPost("sign-in")]
        public async Task<IActionResult> SigninAsync(SigninCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("token/refresh")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

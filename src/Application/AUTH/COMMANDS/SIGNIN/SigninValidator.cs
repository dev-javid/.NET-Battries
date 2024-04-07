using Domain.Common.ValueObjects;

namespace $safeprojectname$.Auth.Commands.Signin
{
    internal class SigninValidator : AbstractValidator<SigninCommand>
    {
        public SigninValidator(IIdentityService identityService)
        {
            RuleFor(x => x)
                .MustAsync(async (x, cancellationToken) =>
                {
                    Email email = Email.From(x.Email);
                    return await identityService.VerifyCredentialsAsync(email, x.Password);
                })
                .WithMessage("Invalid credentials.");
        }
    }
}

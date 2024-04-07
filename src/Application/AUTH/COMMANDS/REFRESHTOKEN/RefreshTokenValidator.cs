namespace $safeprojectname$.Auth.Commands.RefreshToken
{
    internal class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator(IIdentityService identityService, IJwtService jwtService, IDbContext dbContext)
        {
            RuleFor(x => x)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var email = jwtService.ExtractEmailFromToken(x.AccessToken);
                    var user = await identityService.GetUserAsync(email);
                    if (user != null)
                    {
                        return await identityService.ValidateRefreshTokenAsync(user, x.RefreshToken);
                    }

                    return false;
                })
                .WithMessage("Token expired or invalid.");
        }
    }
}

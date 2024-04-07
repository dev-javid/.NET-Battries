using Domain.Common;
using Domain.Identity;

namespace $safeprojectname$.Users.Commands.AddUser;

public class AddUserCommand : IAddCommand
{
    public required string FirstName { get; init; }

    public required string Email { get; init; }

    public required string PhoneNumber { get; init; }

    public required bool IsAdmin { get; init; }

    public class Handler(IIdentityService identityService, IDbContext dbContext) : IAddCommandHandler<AddUserCommand>
    {
        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            using var transaction = dbContext.Database.BeginTransactionAsync(cancellationToken);

            var role = request.IsAdmin ?
                await identityService.GetAdminRoleAsync() :
                await identityService.GetEmployeeRoleAsync();

            var user = User.Create(request.Email.ToEmail(), request.PhoneNumber.ToPhoneNumber());
            var claims = new Dictionary<string, string>
            {
                { "FirstName", request.FirstName },
            };

            user.AddClaims(claims);
            await identityService.CreateUserAsync(user);
            await identityService.AssignRoleAsync(user, role);
            return user.Id;
        }
    }
}

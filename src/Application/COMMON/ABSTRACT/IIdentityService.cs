using System.Security.Claims;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace $safeprojectname$.Common.Abstract;

public interface IIdentityService
{
    Task<bool> VerifyCredentialsAsync(Email email, string password);

    Task<string> GenerateRefreshTokenAsync(User user);

    Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken);

    Task<bool> LogoutAsync(User user);

    Task<bool> CreateUserAsync(User user);

    Task<IList<Claim>> GetClaimsAsync(User user);

    Task SigninAsync(User user);

    Task<User?> GetUserAsync(Email email);

    Task<IdentityRole<int>> GetEmployeeRoleAsync();

    Task<IdentityRole<int>> GetAdminRoleAsync();

    Task<bool> AssignRoleAsync(User user, IdentityRole<int> role);
}

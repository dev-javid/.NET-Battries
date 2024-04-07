using System.Security.Claims;
using Serilog;

namespace $safeprojectname$.Services;

internal class IdentityService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, SignInManager<User> signInManager) : IIdentityService
{
    public const string Administartor = "admin";
    public const string Employee = "employee";
    public const string TokenProviderName = "HRMS.Pro";
    private const string RefreshTokenName = "RefreshToken";

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        await userManager.RemoveAuthenticationTokenAsync(user, TokenProviderName, RefreshTokenName);
        var refreshToken = await userManager.GenerateUserTokenAsync(user, TokenProviderName, RefreshTokenName);
        await userManager.SetAuthenticationTokenAsync(user, TokenProviderName, RefreshTokenName, refreshToken);
        return refreshToken;
    }

    public async Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken)
    {
        return await userManager.VerifyUserTokenAsync(user, TokenProviderName, RefreshTokenName, refreshToken);
    }

    public async Task<bool> LogoutAsync(User user)
    {
        var result = await userManager.UpdateSecurityStampAsync(user);
        if (!result.Succeeded)
        {
            Log.Error(result.ToString());
        }

        return result.Succeeded;
    }

    public async Task<bool> VerifyCredentialsAsync(Email email, string password)
    {
        var user = await userManager.FindByEmailAsync(email.Value);
        return user != null && await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<Claim>> GetClaimsAsync(User user)
    {
        return await userManager.GetClaimsAsync(user);
    }

    public async Task SigninAsync(User user)
    {
        await signInManager.SignInAsync(user, false);
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        var result = await userManager.CreateAsync(user);
        return result.Succeeded;
    }

    public async Task<User?> GetUserAsync(Email email)
    {
        return await userManager.FindByEmailAsync(email.Value);
    }

    public async Task<IdentityRole<int>> GetEmployeeRoleAsync()
    {
        return await roleManager.FindByNameAsync(Administartor)
            ?? throw new NullReferenceException($"Role {Administartor} not found in database");
    }

    public async Task<IdentityRole<int>> GetAdminRoleAsync()
    {
        return await roleManager.FindByNameAsync(Employee)
            ?? throw new NullReferenceException($"Role {Employee} not found in database");
    }

    public async Task<bool> AssignRoleAsync(User user, IdentityRole<int> role)
    {
        var result = await userManager.AddToRoleAsync(user, role.Name!);
        return result.Succeeded;
    }
}

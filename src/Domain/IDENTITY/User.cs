using Microsoft.AspNetCore.Identity;

namespace $safeprojectname$.Identity;

public class User : IdentityUser<int>
{
    private User()
    {
        Claims = [];
    }

    public ICollection<IdentityUserClaim<int>> Claims { get; private set; }

    public static User Create(Email email, PhoneNumber phoneNumber)
    {
        var user = new User
        {
            UserName = email.Value,
            Email = email.Value,
            PhoneNumber = phoneNumber.Value
        };

        return user;
    }

    public void AddClaims(IDictionary<string, string> claims)
    {
        foreach (var claim in claims)
        {
            Claims.Add(new IdentityUserClaim<int>
            {
                UserId = Id,
                ClaimType = claim.Key,
                ClaimValue = claim.Value
            });
        }
    }
}

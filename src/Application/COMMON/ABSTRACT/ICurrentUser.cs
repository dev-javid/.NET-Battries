namespace $safeprojectname$.Common.Abstract;

public interface ICurrentUser
{
    int Id { get; }

    Email Email { get; }

    bool IsSuperAdmin { get; }
}

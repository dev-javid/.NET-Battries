namespace $safeprojectname$.Services;

internal class CurrentUser : ICurrentUser
{
    public int Id => 1;

    public Email Email => throw new NotImplementedException();

    public bool IsSuperAdmin => throw new NotImplementedException();
}

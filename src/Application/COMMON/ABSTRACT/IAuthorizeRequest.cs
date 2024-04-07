namespace $safeprojectname$.Common.Abstract
{
    internal interface IAuthorizeRequest<out TResponse> : IRequest<TResponse>
    {
        Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext);
    }
}

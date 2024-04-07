namespace $safeprojectname$.Common.Abstract;

public interface IUpdateCommand : IRequest<bool>
{
}

public interface IUpdateCommandHandler<TResponse> : IRequestHandler<TResponse, bool> where TResponse : IUpdateCommand
{
}

public interface IDeleteCommand : IRequest<bool>
{
}

public interface IDeleteCommandHandler<TResponse> : IRequestHandler<TResponse, bool> where TResponse : IDeleteCommand
{
}

public interface IAddCommand : IRequest<int>
{
}

public interface IAddCommandHandler<TResponse> : IRequestHandler<TResponse, int> where TResponse : IAddCommand
{
}

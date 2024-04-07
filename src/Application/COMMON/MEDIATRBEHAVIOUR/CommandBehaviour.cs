using Domain.Common;

namespace $safeprojectname$.Common.MediatRBehaviour
{
    public class CommandBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
          where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IUpdateCommand || request is IDeleteCommand)
            {
                var response = await next();
                if (!Convert.ToBoolean(response))
                {
                    throw CustomException.WithInternalServer("Unable to process the request. Most probably nothing was changed in the system.");
                }

                return response;
            }

            if (request is IAddCommand)
            {
                var response = await next();
                if (response is null || Convert.ToInt32(response) <= 0)
                {
                    throw CustomException.WithInternalServer("Unable to process the request. Most probably nothing was changed in the system.");
                }

                return response;
            }
            else
            {
                return await next();
            }
        }
    }
}

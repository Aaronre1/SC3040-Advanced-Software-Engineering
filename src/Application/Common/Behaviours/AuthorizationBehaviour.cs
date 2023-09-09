using System.Reflection;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUser _user;

    public AuthorizationBehaviour(IUser user)
    {
        _user = user;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttribute = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttribute.Any())
        {
            if (_user.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }
        }

        return await next();
    }
}

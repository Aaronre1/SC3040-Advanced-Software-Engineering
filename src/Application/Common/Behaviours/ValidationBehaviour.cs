
using ASE3040.Application.Common.Models;

namespace ASE3040.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                if (typeof(TResponse) == typeof(Result))
                {
                    var response = (TResponse)Activator.CreateInstance(
                        typeof(TResponse),
                        false,
                        failures.Select(x => x.ErrorMessage))!;
                    return response;
                }
                throw new ValidationException(failures);
            }
                
        }
        return await next();
    }
}
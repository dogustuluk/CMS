using FluentValidation;
using MediatR;

namespace CMS.Auth.Features.Common;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var fails = _validators.Select(a => a.Validate(context)).SelectMany(result => result.Errors).Where(a => a != null).ToList();

            if (fails.Count != 0)
            {
                //hatayı daha spesifik yap.
                throw new ValidationException(fails);
            }
        }

        return await next();
    }
}

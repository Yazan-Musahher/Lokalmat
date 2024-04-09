using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Payments.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validators;

    public ValidationBehavior(IValidator<TRequest>? validators = null)
    {
        _validators = validators;
    }

   public async Task<TResponse> Handle(
    TRequest request, 
    RequestHandlerDelegate<TResponse> next, 
    CancellationToken cancellationToken)
    {
        if (_validators is null)
        {
            return await next();
        }
        var validationsResult = await _validators.ValidateAsync(
            request, cancellationToken);

        if (validationsResult.IsValid)
        {
            return await next();
        }

        var errors = validationsResult.Errors
            .ConvertAll(ValidationFailure => 
            Error.Validation(
                ValidationFailure.PropertyName, 
                ValidationFailure.ErrorMessage));
                
        return (dynamic)errors;
    }

}
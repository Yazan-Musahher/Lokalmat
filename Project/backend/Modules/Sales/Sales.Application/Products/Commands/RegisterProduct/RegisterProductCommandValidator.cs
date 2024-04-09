using FluentValidation;

namespace Sales.Application.Commands.RegisterProduct;

public class RegisterProductCommandValidator : AbstractValidator<RegisterProductCommand>
{
    public RegisterProductCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Product Disceripion is required");
    }
}
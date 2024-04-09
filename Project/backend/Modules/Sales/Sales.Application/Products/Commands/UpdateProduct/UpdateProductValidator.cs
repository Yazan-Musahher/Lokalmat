using FluentValidation;

namespace Sales.Application.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.UnitPrice).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}

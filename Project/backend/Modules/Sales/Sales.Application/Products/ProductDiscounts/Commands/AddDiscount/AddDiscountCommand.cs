using ErrorOr;
using MediatR;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.Commands.AddDiscount;

public record AddProductDiscountCommand(
    Guid ProductId,
    decimal DiscountPercentage,
    DateTime StartDate,
    DateTime EndDate,
    Guid? CategoryId

    ) : IRequest<ErrorOr<ProductDiscount>>;

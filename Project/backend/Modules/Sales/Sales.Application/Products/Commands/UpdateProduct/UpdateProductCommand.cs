using ErrorOr;

using MediatR;

namespace Sales.Application.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    string ProductName,
    decimal? UnitPrice, 
    string Description
    ) : IRequest<ErrorOr<Success>>;

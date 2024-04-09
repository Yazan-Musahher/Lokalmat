using ErrorOr;
using MediatR;
using Sales.Domain.ProductAggregate;

namespace Sales.Application.Queries.GetProductDetails;

public record GetProductDetailsQuery(Guid ProductId) : IRequest<ErrorOr<Product>>;

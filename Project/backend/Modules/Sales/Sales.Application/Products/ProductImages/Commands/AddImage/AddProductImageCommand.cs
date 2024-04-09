using ErrorOr;
using MediatR;
using Sales.Domain.ProductAggregate.Entities;
namespace Sales.Application.Commands.AddProductImage.Add;

public record AddProductImageCommand(
    Guid ProductId,
    string Url,
    string Alt,
    string Title,
    string ContentType
) : IRequest<ErrorOr<ProductImage>>;
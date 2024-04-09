using ErrorOr;
using MediatR;

namespace Sales.Application.Commands.AddProductImage;

public record UpdateProductImageCommand(
    Guid ProductId,
    Guid ImageId,
    string Url,
    string Alt,
    string Title,
    string ContentType
) : IRequest<ErrorOr<Success>>;

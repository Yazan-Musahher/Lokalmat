using ErrorOr;
using MediatR;

namespace Sales.Application.Commands.AddProductImage.Delete;

// Delete Product Image Command
public record DeleteProductImageCommand(Guid ProductId, Guid ImageId): IRequest<ErrorOr<Success>>;
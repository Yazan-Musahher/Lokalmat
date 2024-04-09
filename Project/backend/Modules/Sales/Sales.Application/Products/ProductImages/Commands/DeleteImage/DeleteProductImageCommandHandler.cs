using ErrorOr;
using MediatR;
using Sales.Application.Interfaces.Persistence;

namespace Sales.Application.Commands.AddProductImage.Delete;


// Delete Product Image Command handler

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, ErrorOr<Success>>
{
    private readonly IProductRepository _productRepository;
    
    public DeleteProductImageCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ErrorOr<Success>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        // get product by id
        var productResult = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        // if product not found return error
        if (productResult is null)
        {
            return Error.NotFound("Product not found");
        }

        // Delete image in product aggreate business logic
        var result = productResult.DeleteProductImage(request.ImageId);

        // if image not found return error
        if (result.IsError)
        {
            return Error.NotFound("Image not found");
        }

        // Delete the image in the database
        await _productRepository.DeleteProductImageAsync(productResult, request.ImageId, cancellationToken);

        return Result.Success;
    }
}
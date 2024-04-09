using ErrorOr;
using MediatR;
using Sales.Application.Interfaces.Persistence;

namespace Sales.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Success>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Get the product by id
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        // Validate if the product exists
        if (product is null)
        {
            return Error.NotFound($"Product with id {request.ProductId} not found");
        }

        // Business logic to update the product
        var result = product.UpdateProduct(
            request.ProductName, 
            request.UnitPrice, 
            request.Description);

        // Return error if the product is not valid
        if (result.IsError)
        {
            return result;
        }

        // Update the product in the database
        await _productRepository.UpdateProductAsync(product.Id.Value, 
            product.ProductName, 
            product.UnitPrice, 
            product.Description, 
            cancellationToken);

        return result;
    }
}
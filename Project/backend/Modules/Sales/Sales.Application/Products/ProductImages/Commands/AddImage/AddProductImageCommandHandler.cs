using ErrorOr;
using MediatR;
using Sales.Application.Interfaces.Persistence;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.Commands.AddProductImage.Add;

public class AddProductImageCommandHandler : 
    IRequestHandler<AddProductImageCommand, ErrorOr<ProductImage>>
{
    private readonly IProductRepository _productRepository;
    
    public AddProductImageCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<ProductImage>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        
        // get product by id
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        if (product is null)
        {
            return Error.NotFound($"Product with id {request.ProductId} not found");
        }

        // Add product image to product aggreate
        var productImage = ProductImage.Create(
            request.Url, 
            request.Alt, 
            request.Title, 
            request.ContentType);
        
        // Add the image directly via the repository
        var result =  product.AddProductImage(productImage);

        if (result.IsError)
        {
            return Error.NotFound("Product not found");
        }

        await _productRepository.AddProductImageAsync(request.ProductId, productImage);

        return productImage;
    }
}
 
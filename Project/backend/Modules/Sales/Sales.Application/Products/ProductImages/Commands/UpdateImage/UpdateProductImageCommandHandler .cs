namespace Sales.Application.Commands.AddProductImage;
using ErrorOr;
using MediatR;
using Sales.Application.Interfaces.Persistence;

public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand, ErrorOr<Success>>
{
    private readonly IProductRepository _productRepository;
    
    public UpdateProductImageCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        
        // get product by id
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        if (product is null)
        {
            return Error.NotFound($"Product with id {request.ProductId} not found");
        }

        // Update image in product aggreate business logic
        var result = product.UpdateProductImage(
            request.ImageId,
            request.Url, 
            request.Alt, 
            request.Title, 
            request.ContentType
        );

        if (result.IsError)
        {
            return result;
        }

        // Update the image in the database
        await _productRepository.UpdateProductImageAsync(
            request.ProductId,
            request.ImageId,
            request.Url,
            request.Alt,
            request.Title,
            request.ContentType
        );

        return result;
    }
}
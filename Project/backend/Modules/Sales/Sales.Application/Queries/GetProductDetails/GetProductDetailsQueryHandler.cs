using ErrorOr;
using MediatR;

using Sales.Application.Interfaces.Persistence;
using Sales.Domain.ProductAggregate;
namespace Sales.Application.Queries.GetProductDetails;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductDetailsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Error.NotFound("Product not found.");
        }

        return product;
    }
}

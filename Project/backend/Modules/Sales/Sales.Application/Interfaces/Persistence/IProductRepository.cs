using ErrorOr;
using Sales.Domain.ProductAggregate;
using Sales.Domain.ProductAggregate.Entities;

namespace Sales.Application.Interfaces.Persistence;

public interface IProductRepository
{
    public Task AddProductAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateProductAsync(Guid productId,
    string productName, decimal? unitPrice, string description, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateProductAsync(Product product, CancellationToken cancellationToken);
    public Task<ErrorOr<Success>> AddProductImageAsync(Guid id, ProductImage productImage);
    public Task<ErrorOr<Success>> UpdateProductImageAsync(
    Guid productId, Guid imageId, string url, string alt, string title, string contentType);
    Task<ErrorOr<Success>> DeleteProductImageAsync(Product product, Guid Id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateProductInventoryAsync(Guid productId, decimal? unitsInStock, decimal? unitsOnOrder, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);

}

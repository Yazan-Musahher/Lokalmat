
namespace Sales.Contracts.Product.ProductImage.Add;

public record AddProductImageRequest(
    Guid ProductId,
    string Url, 
    string Alt,
    string Title,
    string ContentType
    );


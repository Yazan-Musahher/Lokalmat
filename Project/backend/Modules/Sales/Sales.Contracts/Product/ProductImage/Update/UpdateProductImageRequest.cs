namespace Sales.Contracts.Product.ProductImage.Update;

 public record UpdateProductImageRequest(
        string Url,
        string Alt,
        string Title,
        string ContentType
    );
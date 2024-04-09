namespace Sales.Contracts.Product.ProductImage.Delete
{
    public record DeleteProductImageRequest(Guid ProductId, Guid ImageId);
}
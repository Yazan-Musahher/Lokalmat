namespace Sales.Contracts.Product.ProductImage.Delete
{
    public record DeleteProductImageResponse(
        string ProductId, 
        string ImageId,
        bool IsDeleted
        );
}
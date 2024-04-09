namespace Sales.Contracts.Product.ProductImage.Update
{
    public record UpdateProductImageResponse(
        string ProductId,
        string Id,
        bool Success
    );
}

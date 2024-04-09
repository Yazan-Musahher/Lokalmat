namespace Sales.Contracts.Product
{
    public record UpdateProductDetailsResponse(
        string ProductId,
        bool Success
        );
}
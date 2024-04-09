namespace Sales.Contracts.Product;

public record UpdateProductDetailsRequest(
    string ProductName,
    decimal UnitPrice,
    string Description
    );

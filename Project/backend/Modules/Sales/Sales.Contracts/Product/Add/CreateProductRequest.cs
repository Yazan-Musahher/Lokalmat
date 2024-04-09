namespace Sales.Contracts.Product;

public record CreateProductRequest(
    string ProductName,
    decimal? UnitPrice, 
    string Description,
    Guid CategoryId,
    List<ProductDiscountRequest> ProductDiscounts, 
    List<Image> Images,
    List<Inventory> Inventories,
    List<Status> StatusHistories
    );

public record ProductDiscountRequest(
    decimal DiscountPercentage,
    DateTime StartDate,
    DateTime EndDate,
    Guid? CategoryId
    );

public record Image(
    string Url, 
    string Alt,
    string Title,
    string ContentType
    );

public record Inventory(
    decimal? UnitsInStock,
    decimal? UnitsOnOrder,
    decimal? ReorderLevel,
    string Location
    );

public record Status(
    string ProductStatus,
    string Description
    );


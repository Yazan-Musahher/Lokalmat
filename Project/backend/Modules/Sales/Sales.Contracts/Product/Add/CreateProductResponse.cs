namespace Sales.Contracts.Product;

public record CreateProductResponse(
    string Id, 
    string ProductName,
    decimal? UnitPrice, 
    string Description,
    string CategoryId, 
    float? AverageRating,
    List<ProductDiscountResponse> ProductDiscounts,
    List<ImagesResponse> Images,
    List<InventoryResponse> Inventories,
    List<ProductStatusResponse> StatusHistories
    );

public record ProductDiscountResponse(
    string Id,
    decimal DiscountPercentage,
    DateTime StartDate,
    DateTime EndDate,
    string CategoryId
    );
public record ImagesResponse( 
    string Id,
    string Url, 
    string Alt,
    string Title,
    string ContnetType
    );

public record InventoryResponse(
    string Id,
    decimal UnitsInStock,
    decimal UnitsOnOrder,
    decimal ReorderLevel,
    string Location
    );

public record ProductStatusResponse(
    string Id,
    string Status,
    string Description
    );



namespace Sales.Contracts.Product.Discount;

public record AddProductDiscountRequest(
    Guid ProductId,
    decimal DiscountPercentage,
    DateTime StartDate,
    DateTime EndDate,
    Guid? CategoryId
    );
namespace Sales.Contracts.Product.Discount;

public record AddProductDiscountResponse(
    Guid ProductId,
    Guid DiscountId,
    decimal DiscountPercentage,
    DateTime StartDate,
    DateTime EndDate
    );
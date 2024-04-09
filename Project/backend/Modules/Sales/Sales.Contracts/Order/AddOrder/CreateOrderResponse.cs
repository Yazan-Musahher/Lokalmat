namespace Sales.Contracts.Order;

public record CreateOrderResponse(
    string OrderId,
    string CustomerId,
    ShippingAddress ShippingAddressResponse,
    BillingAddress BillingAddressResponse,
    List<OrderItemResponse> OrderItemsResponse,
    string ShippingMethod,
    decimal TotalAmount
    );

public record OrderItemResponse(
    string Id,
    string ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
    );






namespace Sales.Contracts.Order;

public record CreateOrderRequest(
    string CustomerId,
    ShippingAddress ShippingAddressRequest,
    BillingAddress BillingAddressRequest,
    List<OrderItemRequest> OrderItemsRequest,
    string ShippingMethod
    );

public record ShippingAddress(
    string StreetName,
    string StreetNumber,
    string PostalCode,
    string City,
    string AdditionalDetails
    );

public record BillingAddress(
    string Name,
    string StreetName,
    string StreetNumber,
    string PostalCode,
    string City,
    string AdditionalDetails
    );

public record OrderItemRequest(
    string ProductId,
    int Quantity
    );

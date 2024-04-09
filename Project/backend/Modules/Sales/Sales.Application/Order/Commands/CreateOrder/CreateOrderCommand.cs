using ErrorOr;
using MediatR;
using Sales.Domain.OrderAggregate;

namespace Sales.Application.Commands.CreateOrder;


public record CreateOrderCommand(
    Guid CustomerId,
    string OrderDescription,
    ShippingAddressCommand ShippingAddress,
    BillingAddressCommand BillingAddress,
    List<OrderItemCommand> OrderItems,
    string ShippingMethod
     ) : IRequest<ErrorOr<OrderAggregate>>;


public record ShippingAddressCommand(
    string StreetName,
    string StreetNumber,
    string PostalCode,
    string City,
    string AdditionalDetails
    );

public record BillingAddressCommand(
    string Name,
    string StreetName,
    string StreetNumber,
    string PostalCode,
    string City,
    string AdditionalDetails
    );

public record OrderItemCommand(
    Guid productId,
    int quantity,
    string description
    );
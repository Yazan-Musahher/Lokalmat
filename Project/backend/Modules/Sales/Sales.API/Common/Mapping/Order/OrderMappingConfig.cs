using Mapster;
using Sales.Application.Commands.CreateOrder;
using Sales.Contracts.Order;
using Sales.Domain.OrderAggregate;
using Sales.Domain.OrderAggregate.Entities;
namespace Sales.API.Common.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Mapping from CreateOrderRequest to CreateOrdercommand
        config.NewConfig<CreateOrderRequest, CreateOrderCommand>()
            .Map(dest => dest.CustomerId, src => src.CustomerId)
            .Map(dest => dest.ShippingAddress, src => src.ShippingAddressRequest)
            .Map(dest => dest.BillingAddress, src => src.BillingAddressRequest)
            .Map(dest => dest.OrderItems, src => src.OrderItemsRequest)
            .Map(dest => dest.ShippingMethod, src => src.ShippingMethod);

        config.NewConfig<OrderItem, OrderItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.ProductId, src => src.ProductId.Value);
        // Mapping from Entities to CreateOrderResponse
        config.NewConfig<OrderAggregate, CreateOrderResponse>()
            .Map(dest => dest.OrderId, src => src.Id.Value)
            .Map(dest => dest.CustomerId, src => src.CustomerId.Value)
            .Map(dest => dest.ShippingAddressResponse, src => src.ShippingAddress)
            .Map(dest => dest.BillingAddressResponse, src => src.BillingAddress)
            .Map(dest => dest.OrderItemsResponse, src => src.OrderItems)
            .Map(dest => dest.ShippingMethod, src => src.ShippingMethod)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount);
    }
}

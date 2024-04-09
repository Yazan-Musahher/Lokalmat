using Mapster;
using Sales.Domain.OrderAggregate;
using Sales.Domain.OrderAggregate.Entities;
using Sales.Domain.OrderAggregate.ValueObjects;
using Sales.Domain.ProductAggregate.ValueObjects;
using Sales.Infrastructure.Models;


namespace Order.Infrastructure.Presistence.Configurations;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BillingAddressDto, BillingAddress>()
            .MapWith(dto => BillingAddress.Create(
                dto.Name ?? string.Empty,
                dto.StreetName ?? string.Empty,
                dto.StreetNumber ?? string.Empty,
                dto.PostalCode ?? string.Empty,
                dto.City ?? string.Empty,
                dto.AdditionalDetails ?? string.Empty));
        
        config.NewConfig<ShippingAddressDto, ShippingAddress>()
            .MapWith(dto => ShippingAddress.Create(
                dto.StreetName ?? string.Empty,
                dto.StreetNumber ?? string.Empty,
                dto.PostalCode ?? string.Empty,
                dto.City ?? string.Empty,
                dto.AdditionalDetails ?? string.Empty));

        config.NewConfig<ShippingAddress, ShippingAddressDto>()
            .Map(dest => dest.StreetName, src => src.StreetName)
            .Map(dest => dest.StreetNumber, src => src.StreetNumber)
            .Map(dest => dest.PostalCode, src => src.PostalCode)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.AdditionalDetails, src => src.AdditionalDetails);

        
        config.NewConfig<OrderItemDto, OrderItem>()
            .MapWith(dto => OrderItem.Create(
                OrderItemId.Create(dto.Id),
                ProductId.Create(dto.ProductId),
                dto.ProductName ?? string.Empty,
                dto.Quantity,
                dto.UnitPrice,
                dto.Description ?? string.Empty)
                );
        config.NewConfig<OrderItem, OrderItemDto>()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.ProductId, src => src.ProductId.Value)
        .Map(dest => dest.ProductName, src => src.ProductName)
        .Map(dest => dest.Quantity, src => src.Quantity)
        .Map(dest => dest.UnitPrice, src => src.UnitPrice)
        .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<OrderAggregate, OrderAggregateDto>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.CustomerId, src => src.CustomerId.Value)
            .Map(dest => dest.OrderDate, src => src.OrderDate)
            .Map(dest => dest.OrderStatus, src => src.OrderStatus)
            .Map(dest => dest.Address, src => src.ShippingAddress)
            .Map(dest => dest.BillingAddress, src => src.BillingAddress)
            .Map(dest => dest.OrderItems, src => src.OrderItems != null
                ? src.OrderItems.Select(x => x.Adapt<OrderItemDto>()).ToList() : new List<OrderItemDto>())
            .Map(dest => dest.ShippingMethod, src => src.ShippingMethod)
            .Map(dest => dest.PaymentStatus, src => src.PaymentStatus)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount);
            

        config.NewConfig<OrderAggregateDto, OrderAggregate>()
            .MapWith(dto => OrderAggregate.Create(
                OrderId.Create(dto.Id),
                CustomerId.Create(dto.CustomerId),
                dto.Address != null ? dto.Address.Adapt<ShippingAddress>() : ShippingAddress.Create(
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),
                dto.BillingAddress != null ? dto.BillingAddress.Adapt<BillingAddress>() : BillingAddress.Create(
                    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty),
                dto.OrderItems != null ? dto.OrderItems.Select(x => x.Adapt<OrderItem>()).ToList() : new List<OrderItem>(),
                dto.PaymentStatus,
                dto.ShippingMethod,
                dto.OrderDate));
                
    }
}
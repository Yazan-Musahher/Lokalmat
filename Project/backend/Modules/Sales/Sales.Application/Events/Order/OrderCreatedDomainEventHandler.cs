
using MediatR;
using Sales.Application.interfaces;
using Sales.Application.Interfaces;
using Sales.Application.Interfaces.Persistence;
using Sales.Domain.Events;
using Sales.Domain.ProductAggregate;
using Sales.IntegrationEvents.Events;

namespace Sales.Application.Events;

internal sealed class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    private readonly IIntegrationEventsPublisher _eventBus;

    public OrderCreatedDomainEventHandler(IOrderRepository orderRepository, IProductRepository productRepository, IIntegrationEventsPublisher eventBus)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.FindByIdAsync(notification.OrderId, cancellationToken);
        if (order is null)
        {
            throw new ApplicationException($"Order with id {notification.OrderId} not found.");
        }
        
        if (await _productRepository.GetByIdAsync(notification.ProductId, cancellationToken) is not Product product)
        {
            throw new ApplicationException($"Product with id {notification.ProductId} not found.");
        }

        var orderItem = order._orderItems.FirstOrDefault(item => item.ProductId.Value == notification.ProductId);
        if (orderItem is null)
        {
            throw new ApplicationException($"No order item found for product id {notification.ProductId} in order {notification.OrderId}.");
        }

        foreach (var inventory in product.Inventories)
        {
            decimal newUnitsInStock = inventory.UnitsInStock.HasValue ? inventory.UnitsInStock.Value - orderItem.Quantity : 0;
            decimal newUnitsOnOrder = inventory.UnitsOnOrder.HasValue ? inventory.UnitsOnOrder.Value : 0;
            inventory.UpdateUnitsInStock(newUnitsInStock, newUnitsOnOrder);
        }
        
        await _productRepository.UpdateProductAsync(product, cancellationToken);

        await _eventBus.PublishAsync(new OrderCreatedIntegrationEvent(notification.OrderId, notification.CustomerId, notification.ProductId), cancellationToken);
    }
}
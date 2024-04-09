using MediatR;

using Microsoft.Extensions.Logging;

using Sales.IntegrationEvents.Events;

using TransportHub.Application.Interfaces;

namespace TransportHub.Application.Events;

internal class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedIntegrationEvent>
{
    private readonly ITransporterRepository _transporterRepository;
        private readonly ILogger<OrderCreatedDomainEventHandler> _logger;

     public OrderCreatedDomainEventHandler(ITransporterRepository transporterRepository, ILogger<OrderCreatedDomainEventHandler> logger)
    {
        _transporterRepository = transporterRepository;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification == null) throw new ArgumentNullException(nameof(notification));

        _logger.LogInformation($"Handling OrderCreatedDomainEvent for OrderId: {notification.OrderId}, CustomerId: {notification.CustomerId}, ProductId: {notification.ProductId}");

        try
        {
            await _transporterRepository.AddShippingOrder(notification.OrderId, notification.CustomerId, notification.ProductId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling OrderCreatedDomainEvent for OrderId: {OrderId}", notification.OrderId);
            
        }
    }
}


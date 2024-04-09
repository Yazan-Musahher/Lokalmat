using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Payments.IntegrationEvents.Events;

using Sales.Application.interfaces;

namespace Sales.Application.Events.Payment
{
    public class PaymentCreatedIntegrationEventHandler : INotificationHandler<PaymentCreatedIntegrationEvent>
    {
        private readonly ILogger<PaymentCreatedIntegrationEventHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public PaymentCreatedIntegrationEventHandler(
            ILogger<PaymentCreatedIntegrationEventHandler> logger,
            IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task Handle(PaymentCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @notification.PaymentId, "Sales", @notification);

            var order = await _orderRepository.FindByIdAsync(notification.OrderId, cancellationToken);

            if (order is null)
            {
                _logger.LogWarning("----- Order not found with Id: {OrderId}", notification.OrderId);
                Error.NotFound("Order not found");
                return;
            }

            order.UpdatePaymentStatus(notification.PaymentStatus);

            await _orderRepository.UpdateAsync(order, cancellationToken);


        }
    }
}
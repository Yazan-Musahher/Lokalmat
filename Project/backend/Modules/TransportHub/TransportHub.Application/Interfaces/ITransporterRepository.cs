using TransportHub.Domain.TransporterAggregate;
namespace TransportHub.Application.Interfaces;


public interface ITransporterRepository
{
    Task AddTransporterAsync(TransporterAggregate transporter, CancellationToken cancellationToken);

    Task AddShippingOrder(Guid orderId, Guid customerId, Guid productId, CancellationToken cancellationToken);
}

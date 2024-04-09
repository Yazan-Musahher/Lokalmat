using Mapster;
using MongoDB.Driver;
using TransportHub.Application.Interfaces;
using TransportHub.Domain.TransporterAggregate;
using TransportHub.Infrastructure.Persistence.Models;

namespace TransportHub.Infrastructure.Persistence.Repositories;

public class TransporterRepository : ITransporterRepository
{
    private readonly IMongoCollection<TransporterDto> _transporters;
    private readonly IMongoCollection<ShippingOrderDto> _shippingOrders;
    private readonly IUnitOfWork _unitOfWork;

    public TransporterRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _transporters = database.GetCollection<TransporterDto>("TransporterDocument");
        _shippingOrders = database.GetCollection<ShippingOrderDto>("ShippingDetailsDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddShippingOrder(Guid orderId, Guid customerId, Guid productId, CancellationToken cancellationToken)
    {
        // add test implementation for AddShippingOrder ShippingDetailsDocument
        var orderDto = new ShippingOrderDto
        {
            OrderId = orderId,
            CustomerId = customerId,
            ProductId = productId
        };
        Task operation = _shippingOrders.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, orderDto);
        await _unitOfWork.AddOperation(operation);
    }

    public async Task AddTransporterAsync(TransporterAggregate transporter, CancellationToken cancellationToken)
    {
        var transporterDto = transporter.Adapt<TransporterDto>();
        Task operation = _transporters.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, transporterDto);
        await _unitOfWork.AddOperation(operation);
        
    }
}

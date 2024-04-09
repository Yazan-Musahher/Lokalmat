using Mapster;
using MongoDB.Driver;
using Sales.Application.interfaces;
using Sales.Application.Interfaces;
using Sales.Domain.OrderAggregate;
using Sales.Infrastructure.Models;

namespace Sales.Infrastructure.Presistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<OrderAggregateDto> _orders;
    private readonly IUnitOfWork _unitOfWork;
    public OrderRepository(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _orders = database.GetCollection<OrderAggregateDto>("OrderDocument");
        _unitOfWork = unitOfWork;
    }

    public async Task AddOrderAsync(OrderAggregate order, CancellationToken cancellationToken)
    {
       //add order to the database
        var orderDto = order.Adapt<OrderAggregateDto>();
        Task operation = _orders.InsertOneAsync(_unitOfWork.Session as IClientSessionHandle, orderDto);
        await _unitOfWork.AddOperation(operation);
    }

    // Find Order by Id
    public async Task<OrderAggregate?> FindByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var orderDto = await _orders.Find(p => p.Id == orderId).SingleOrDefaultAsync(cancellationToken);
        if (orderDto == null) return null;
        return orderDto.Adapt<OrderAggregate>();
    }

    // Update Order
    public async Task UpdateAsync(OrderAggregate order, CancellationToken cancellationToken)
    {
        var orderDto = order.Adapt<OrderAggregateDto>();
        Task operation =  _orders.ReplaceOneAsync(p => p.Id == order.Id.Value, orderDto, cancellationToken: cancellationToken);
        await _unitOfWork.AddOperation(operation);
    }
}

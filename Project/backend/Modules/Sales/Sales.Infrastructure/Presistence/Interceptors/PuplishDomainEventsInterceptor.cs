using Newtonsoft.Json;
using MongoDB.Driver;
using LokalProdusert.Shared.Outbox;
using Sales.Application.Interfaces;
using Sales.Domain.Models;

namespace Sales.Infrastructure.Persistence.Interceptors;

public class PuplishDomainEventsInterceptor
{
    private readonly IMongoCollection<OutboxMessage> _outboxCollection;
    private readonly IUnitOfWork _unitOfWork;

    public PuplishDomainEventsInterceptor(IMongoDatabase database, IUnitOfWork unitOfWork)
    {
        _outboxCollection = database.GetCollection<OutboxMessage>("OutboxMessages");
        _unitOfWork = unitOfWork;
    }

    public async Task StoreDomainEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
         var outboxMessages = domainEvents.Select(domainEvent => new OutboxMessage
        {
            Id = Guid.NewGuid(),
            MessageType = domainEvent.GetType().AssemblyQualifiedName ?? string.Empty,
            Message = JsonConvert.SerializeObject(domainEvent),
            OccurredOn = DateTime.UtcNow
        }).ToList();

        if (outboxMessages.Any())
        {
           await _unitOfWork.AddOperation(_outboxCollection.InsertManyAsync(_unitOfWork.Session as IClientSessionHandle, outboxMessages, cancellationToken: cancellationToken));
        }
    }

}
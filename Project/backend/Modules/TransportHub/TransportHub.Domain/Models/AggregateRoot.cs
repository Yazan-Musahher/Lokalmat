
namespace TransportHub.Domain.Models;

public abstract class AggregateRoot<TID> : Entity<TID>
    where TID : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected AggregateRoot(TID id) : base(id)
    {
    }
    

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
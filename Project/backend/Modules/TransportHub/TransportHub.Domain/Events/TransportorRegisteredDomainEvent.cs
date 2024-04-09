using TransportHub.Domain.Models;

namespace TransportHub.Domain.Events
{
    public sealed record TransportorRegisteredDomainEvent(Guid TransportorId, string CompanyName) : IDomainEvent
    {
        public Guid TransportorId { get; init; } = TransportorId;
        public string CompanyName { get; init; } = CompanyName;
    }
}
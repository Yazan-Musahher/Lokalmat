using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.Events;

public class ProductRegisteredEvent : IDomainEvent
{
    public Guid ProductId { get; }
    public Guid CategoryId { get; }
    public DateTime CreatedDateTime { get; }
    
    public ProductRegisteredEvent(Guid productId, Guid categoryId , DateTime createdDateTime)
    {
        ProductId = productId;
        CategoryId = categoryId;
        CreatedDateTime = createdDateTime;
    }
}
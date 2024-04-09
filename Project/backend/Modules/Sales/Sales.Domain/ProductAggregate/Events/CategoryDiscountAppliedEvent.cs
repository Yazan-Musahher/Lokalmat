using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.Events;

public class CategoryDiscountAppliedEvent : IDomainEvent
{
    public Guid DiscountId { get; }
    public Guid CategoryId { get; }
    public decimal DiscountPercentage { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    
    public CategoryDiscountAppliedEvent(Guid categoryId, Guid discountId, decimal discountPercentage, DateTime startDate, DateTime endDate)
    {
        DiscountId = discountId;
        CategoryId = categoryId;
        DiscountPercentage = discountPercentage;
        StartDate = startDate;
        EndDate = endDate;
    }
}

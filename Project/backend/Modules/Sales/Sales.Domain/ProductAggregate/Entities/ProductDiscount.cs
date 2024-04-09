using Sales.Domain.CategoryAggregate.ValueObjects;
using Sales.Domain.Models;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.ProductAggregate.Entities;

public sealed class ProductDiscount : Entity<ProductDiscountId>
{
    public decimal DiscountPercentage { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    //public CategoryId? CategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private ProductDiscount(
        ProductDiscountId productDiscountId,
        decimal discountPercentage,
        DateTime startDate,
        DateTime endDate
        //CategoryId? categoryId
    ) : base(productDiscountId)
    {
        DiscountPercentage = discountPercentage;
        StartDate = startDate;
        EndDate = endDate;
        //CategoryId = categoryId;
        CreatedAt = DateTime.UtcNow;
    }

    public static ProductDiscount Create(
        decimal discountPercentage,
        DateTime startDate,
        DateTime endDate
        //CategoryId? categoryId
    )
    {
        return new ProductDiscount(
            ProductDiscountId.CreateUnique(),
            discountPercentage,
            startDate,
            endDate
            //categoryId
        );
    }

    public static ProductDiscount Create(
        ProductDiscountId productDiscountId,
        decimal discountPercentage,
        DateTime startDate,
        DateTime endDate
        //CategoryId? categoryId
    )
    {
        return new ProductDiscount(
            productDiscountId,
            discountPercentage,
            startDate,
            endDate
        );
    }



}

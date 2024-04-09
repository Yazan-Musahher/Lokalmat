using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.ValueObjects;

public sealed class ProductDiscountId : ValueObject
{
    public Guid Value { get; }

    private ProductDiscountId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ProductCategory id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ProductDiscountId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductDiscountId Create(Guid value)
    {
        return  new ProductDiscountId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

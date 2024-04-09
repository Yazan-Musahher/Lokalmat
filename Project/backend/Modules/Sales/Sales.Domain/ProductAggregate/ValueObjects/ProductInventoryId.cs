using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.ValueObjects;

public sealed class ProductInventoryId : ValueObject
{
    public Guid Value { get; }

    private ProductInventoryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Image id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ProductInventoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductInventoryId Create(Guid value)
    {
        return new ProductInventoryId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

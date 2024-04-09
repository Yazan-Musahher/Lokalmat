using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.ValueObjects;

public sealed class ProductImageId : ValueObject
{
    public Guid Value { get; }

    private ProductImageId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Image id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ProductImageId CreateUnique()
    {
        return new ProductImageId(Guid.NewGuid());
    }

    public static ProductImageId Create(Guid value)
    {
        return new ProductImageId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.ValueObjects;

public sealed class ProductId : ValueObject
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Product id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ProductId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductId Create(Guid value)
    {
        return new ProductId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

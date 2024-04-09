using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.ValueObjects;

public sealed class ProductStatusId : ValueObject
{
    public Guid Value { get; }

    private ProductStatusId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Product Status id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static ProductStatusId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductStatusId Create(Guid value)
    {
        return new ProductStatusId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

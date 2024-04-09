using Sales.Domain.Models;

namespace Sales.Domain.ProductAggregate.ValueObjects;

public sealed class PriceId : ValueObject
{
    public Guid Value { get; }

    private PriceId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Price id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static PriceId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

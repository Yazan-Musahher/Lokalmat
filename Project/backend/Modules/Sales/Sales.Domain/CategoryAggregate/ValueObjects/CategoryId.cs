using Sales.Domain.Models;

namespace Sales.Domain.CategoryAggregate.ValueObjects;

public sealed class CategoryId : ValueObject
{
    public Guid Value { get; }

    private CategoryId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Category id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static CategoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CategoryId Create(Guid value)
    {
        return  new CategoryId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

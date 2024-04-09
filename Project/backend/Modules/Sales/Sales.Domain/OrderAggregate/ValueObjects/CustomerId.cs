using Sales.Domain.Models;

namespace Sales.Domain.OrderAggregate.ValueObjects
{
    public sealed class CustomerId : ValueObject
    {
        public Guid Value { get; }

        private CustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Customer id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static CustomerId CreateUnique()
        {
            return new CustomerId(Guid.NewGuid());
        }

        public static CustomerId Create(Guid value)
        {
            return new CustomerId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
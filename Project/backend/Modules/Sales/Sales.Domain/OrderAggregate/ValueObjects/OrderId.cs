
using Sales.Domain.Models;

namespace Sales.Domain.OrderAggregate.ValueObjects
{
    public sealed class OrderId : ValueObject
    {
        public Guid Value { get; }

        private OrderId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Order id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static OrderId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static OrderId Create(Guid value)
        {
            return new OrderId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
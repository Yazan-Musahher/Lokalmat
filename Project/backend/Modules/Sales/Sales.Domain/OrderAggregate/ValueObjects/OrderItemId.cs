
using Sales.Domain.Models;

namespace Sales.Domain.OrderAggregate.ValueObjects
{
    public sealed class OrderItemId : ValueObject
    {
        public Guid Value { get; }

        private OrderItemId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Order id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static OrderItemId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static OrderItemId Create(Guid value)
        {
            return new OrderItemId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
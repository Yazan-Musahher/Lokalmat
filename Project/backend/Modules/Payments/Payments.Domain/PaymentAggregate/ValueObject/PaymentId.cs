using Payments.Domain.Models;

namespace Payments.Domain.PaymentAggregate;

public sealed class PaymentId : ValueObject
    {
        public Guid Value { get; }

        private PaymentId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Payment id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static PaymentId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static PaymentId Create(Guid value)
        {
            return new PaymentId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
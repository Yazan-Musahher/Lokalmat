
using TransportHub.Domain.Models;

namespace TransportHub.Domain.TransporterAggregate.ValueObjects
{
    public sealed class TransporterId : ValueObject
    {
        public Guid Value { get; }

        private TransporterId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Transporter id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static TransporterId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static TransporterId Create(Guid value)
        {
            return new TransporterId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
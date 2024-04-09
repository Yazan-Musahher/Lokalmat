
using TransportHub.Domain.Models;

namespace TransportHub.Domain.TransporterAggregate.ValueObjects
{
    public sealed class TransporterAddressId : ValueObject
    {
        public Guid Value { get; }

        private TransporterAddressId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Transportor Address id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static TransporterAddressId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static TransporterAddressId Create(Guid value)
        {
            return new TransporterAddressId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
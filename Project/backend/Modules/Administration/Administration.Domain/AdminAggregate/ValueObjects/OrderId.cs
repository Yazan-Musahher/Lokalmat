using Administration.Domain.Models;

namespace Administration.Domain.AdminAggregate.ValueObjects
{
    public sealed class UserId : ValueObject
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
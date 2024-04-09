using Administration.Domain.Models;

namespace Administration.Domain.AdminAggregate.ValueObjects
{
    public sealed class AdminId : ValueObject
    {
        public Guid Value { get; }

        private AdminId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Admin Id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static AdminId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static AdminId Create(Guid value)
        {
            return new AdminId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
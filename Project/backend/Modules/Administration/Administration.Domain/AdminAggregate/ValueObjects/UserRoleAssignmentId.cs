using Administration.Domain.Models;

namespace Administration.Domain.AdminAggregate.ValueObjects
{
    public sealed class UserRoleAssignmentId : ValueObject
    {
        public Guid Value { get; }

        private UserRoleAssignmentId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("User Role Assignment id cannot be empty", nameof(value));
            }

            Value = value;
        }

        public static UserRoleAssignmentId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static UserRoleAssignmentId Create(Guid value)
        {
            return new UserRoleAssignmentId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
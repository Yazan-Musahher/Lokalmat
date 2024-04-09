using Administration.Domain.AdminAggregate.ValueObjects;
using Administration.Domain.Models;

using Users.Domain.UserAggregate.Enums;

namespace Administration.Domain.AdminAggregate.Entities;

public class UserRoleAssignment : Entity<UserRoleAssignmentId>
{
    public UserId UserId { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime AssignedAt { get; private set; }

    private UserRoleAssignment(UserRoleAssignmentId id, UserId userId, UserRole role, DateTime assignedAt) : base(id)
    {
        UserId = userId;
        Role = role;
        AssignedAt = assignedAt;
    }

    public static UserRoleAssignment Create(
        UserRoleAssignmentId id, 
        UserId userId, 
        UserRole role, 
        DateTime assignedAt)
    {
        return new UserRoleAssignment(id, userId, role, assignedAt);
    }

    public static UserRoleAssignment Create(
        UserId userId, 
        UserRole role, 
        DateTime assignedAt)
    {
        return new UserRoleAssignment(
            UserRoleAssignmentId.CreateUnique(), 
            userId, 
            role, 
            assignedAt);
    }

    public void UpdateRole(UserRole role, DateTime updatedAt)
    {
        Role = role;
        AssignedAt = updatedAt;
    }
}
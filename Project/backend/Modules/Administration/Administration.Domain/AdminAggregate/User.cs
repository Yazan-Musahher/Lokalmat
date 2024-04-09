using Administration.Domain.AdminAggregate.Entities;
using Administration.Domain.AdminAggregate.ValueObjects;
using Administration.Domain.Events;
using Administration.Domain.Models;

using Users.Domain.UserAggregate.Enums;

namespace Administration.Domain.AdminAggregate;
public sealed class User : AggregateRoot<UserId>
{
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User(
        UserId UserId,
        string email, 
        DateTime createdAt) : base(UserId ?? UserId.CreateUnique())
    {
        Email = email;
        Role = UserRole.Customer;
        CreatedAt = createdAt;
    }

    public static User Create(
        string email, 
        DateTime createdAt)
    {
        var user = new User(
            UserId.CreateUnique(),
            email,
            createdAt
        );
        return user;
    }

    public static User Create(
        UserId userId,
        string email, 
        DateTime createdAt)
    {
        var user = new User(
            userId,
            email,
            createdAt
        );
        return user;
    }

    // update user role
    public void UpdateRole(UserRole role)
    {
        Role = role;
        RaiseDomainEvent(new UserRoleUpdatedDomainEvent(Id.Value, role.ToString()));
    }
}
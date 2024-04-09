using Payments.Domain.OrderAggregate.ValueObjects;

using Users.Domain.Events;
using Users.Domain.Models;
using Users.Domain.UserAggregate.Enums;

namespace Users.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User(
        UserId UserId,
        string firstName, 
        string lastName, 
        string email, 
        string password,
        UserRole role,
        DateTime createdAt) : base(UserId ?? UserId.CreateUnique())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }

    public static User Create(
        string firstName, 
        string lastName, 
        string email, 
        string password,
        UserRole role,
        DateTime createdAt)
    {
        var user = new User(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password,
            role,
            createdAt
        );

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id.Value, user.Email));
        return user;
    }

    public static User Create(
        UserId userId,
        string firstName, 
        string lastName, 
        string email, 
        string password,
        UserRole role,
        DateTime createdAt)
    {
        var user = new User(
            userId,
            firstName,
            lastName,
            email,
            password,
            role,
            createdAt
        );
        return user;
    }

    // update user role
    public void UpdateRole(UserRole role)
    {
        Role = role;
    }
}
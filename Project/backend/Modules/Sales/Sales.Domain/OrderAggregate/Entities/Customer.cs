using Sales.Domain.Models;
using Sales.Domain.OrderAggregate.ValueObjects;

namespace Sales.Domain.OrderAggregate.Entities;

public sealed class Customer : Entity<CustomerId>
{
    public string? CustomerName { get; private set; }
    public string? CustomerEmail { get; private set; }

    private Customer(CustomerId customerId, string customerName, string customerEmail)
        : base(customerId)
    {
        CustomerName = customerName;
        CustomerEmail = customerEmail;
    }

    public static Customer Create(string customerName, string customerEmail)
    {
        return new(
            CustomerId.CreateUnique(),
            customerName,
            customerEmail
        );
    }

    public static Customer Create(CustomerId customerId, string customerName, string customerEmail)
    {
        return new(
            customerId,
            customerName,
            customerEmail
        );
    }
}

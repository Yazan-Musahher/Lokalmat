using Sales.Domain.Models;
using Sales.Domain.OrderAggregate.ValueObjects;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.OrderAggregate.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }

    public string? ProductName { get; private set; }
    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public string Description { get; private set; }

    private OrderItem(
                    OrderItemId orderItemId, 
                    ProductId productId, 
                    string productName, 
                    int quantity, 
                    decimal unitPrice,
                    string description
                    ) : base(orderItemId)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        }

        if (unitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than 0", nameof(unitPrice));
        }

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Description = description;

    }

    public static OrderItem Create(
        ProductId productId,
        string productName,
        int quantity,
        decimal unitPrice,
        string description)
    {
        return new(
            OrderItemId.CreateUnique(),
            productId,
            productName,
            quantity,
            unitPrice,
            description);
    }

    public static OrderItem Create(
        OrderItemId orderItemId,
        ProductId productId,
        string productName,
        int quantity,
        decimal unitPrice,
        string description)
    {
        return new(
            orderItemId,
            productId,
            productName,
            quantity,
            unitPrice,
            description);
    }
}
 
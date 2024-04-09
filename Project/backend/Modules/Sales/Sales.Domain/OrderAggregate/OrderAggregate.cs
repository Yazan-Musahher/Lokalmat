using Sales.Domain.Events;
using Sales.Domain.Models;
using Sales.Domain.OrderAggregate.Entities;
using Sales.Domain.OrderAggregate.Enums;
using Sales.Domain.OrderAggregate.ValueObjects;
using Sales.Domain.ProductAggregate.ValueObjects;

namespace Sales.Domain.OrderAggregate;

public sealed class OrderAggregate : AggregateRoot<OrderId>
{   
    public CustomerId CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public ShippingAddress ShippingAddress { get; private set; }
    public BillingAddress BillingAddress { get; private set; }
    public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);
    public string ShippingMethod { get; private set; }

    public string PaymentStatus { get; private set; }

    private OrderAggregate(
        OrderId orderId,
        CustomerId customerId,
        ShippingAddress shippingAddress,
        BillingAddress billingAddress,
        List<OrderItem> orderItems,
        string PaymentStatus = "Pending",
        string shippingMethod = "Pickup",
        DateTime? orderDate = null) : base(orderId)
    {
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        _orderItems = new List<OrderItem>(orderItems);
        OrderDate = orderDate ?? DateTime.Now;
        OrderStatus = OrderStatus.Pending;
        this.PaymentStatus = PaymentStatus;
        ShippingMethod = shippingMethod;
    }

    public static OrderAggregate Create(
        CustomerId customerId,
        ShippingAddress shippingAddress,
        BillingAddress billingAddress,
        List<OrderItem> orderItems,
        string PaymentStatus = "Pending",
        string shippingMethod = "Pickup",
        DateTime? orderDate = null)
    {
        var order = new OrderAggregate(
            OrderId.CreateUnique(),
            customerId,
            shippingAddress,
            billingAddress,
            orderItems,
            PaymentStatus,
            shippingMethod,
            orderDate);

        order.RaiseDomainEvent(new OrderCreatedDomainEvent(order.Id.Value, customerId.Value, ProductId.Create(orderItems.First().ProductId.Value).Value));

        return order;
    }

    public static OrderAggregate Create(
        OrderId orderId,
        CustomerId customerId,
        ShippingAddress shippingAddress,
        BillingAddress billingAddress,
        List<OrderItem> orderItems,
        string PaymentStatus,
        string shippingMethod,
        DateTime? orderDate = null)
    {
        return new(
            orderId,
            customerId,
            shippingAddress,
            billingAddress,
            orderItems,
            PaymentStatus,
            shippingMethod,
            orderDate);
    }

    public void UpdatePaymentStatus(string paymentStatus)
    {
        PaymentStatus = paymentStatus;
    }
    
}
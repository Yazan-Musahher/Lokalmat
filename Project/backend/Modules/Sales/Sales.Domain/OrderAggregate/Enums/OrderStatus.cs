namespace Sales.Domain.OrderAggregate.Enums;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Returned,
    Failed
}

namespace server.Models.OrderModule;

public class Order
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
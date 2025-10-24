namespace StoreManagement.Domain.Models;

public class Order
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public int OrderStatusId { get; set; }
    public OrderStatus? OrderStatus { get; set; }

    public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();


}
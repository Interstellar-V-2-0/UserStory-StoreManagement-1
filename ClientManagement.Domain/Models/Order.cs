using Clientmanagement.Domain.Models;

public class Order
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderStatusId { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
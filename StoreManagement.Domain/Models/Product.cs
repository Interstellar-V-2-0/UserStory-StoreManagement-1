namespace StoreManagement.Domain.Models;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
namespace StoreManagement.Api.DTOs;

using StoreManagement.Domain.Models;

public class OrderDetailsReadDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public OrderDetailsReadDto(OrderDetails detail)
    {
        Id = detail.Id;
        OrderId = detail.OrderId;
        ProductId = detail.ProductId;
        Quantity = detail.Quantity;
    }
}

public class OrderDetailsCreateDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public OrderDetails ToOrderDetails()
    {
        return new OrderDetails
        {
            OrderId = OrderId,
            ProductId = ProductId,
            Quantity = Quantity
        };
    }
}

public class OrderDetailsUpdateDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public OrderDetails ToOrderDetails()
    {
        return new OrderDetails
        {
            Id = Id,
            OrderId = OrderId,
            ProductId = ProductId,
            Quantity = Quantity
        };
    }
}
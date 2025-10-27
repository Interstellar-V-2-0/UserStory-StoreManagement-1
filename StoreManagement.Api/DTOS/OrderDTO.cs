using StoreManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreManagement.Api.DTOs;

// DTO para cada detalle de la orden
public class OrderDetailReadDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }

    public OrderDetailReadDto(OrderDetails detail)
    {
        Id = detail.Id;
        ProductId = detail.ProductId;
        ProductName = detail.Product?.Name ?? "";
        ProductPrice = detail.Product?.Price ?? 0;
        Quantity = detail.Quantity;
    }
}

// DTO principal de la orden
public class OrderReadDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public string OrderStatusName { get; set; } = "";
    public List<OrderDetailReadDto> OrderDetails { get; set; } = new();

    public OrderReadDto(Order order)
    {
        Id = order.Id;
        CustomerId = order.CustomerId;
        CustomerName = order.Customer != null ? $"{order.Customer.FirstName} {order.Customer.LastName}" : "";
        OrderDate = order.OrderDate;
        OrderStatusName = order.OrderStatus?.Name ?? "";
        OrderDetails = order.OrderDetails?.Select(od => new OrderDetailReadDto(od)).ToList() ?? new List<OrderDetailReadDto>();
    }
}

// DTO para crear una orden
public class OrderCreateDto
{
    public int CustomerId { get; set; }
    public string OrderStatusName { get; set; } = "";
}

// DTO para actualizar una orden
public class OrderUpdateDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string OrderStatusName { get; set; } = "";
}
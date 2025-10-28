using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Models;
using StoreManagement.Api.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        var dtos = orders.Select(o => new OrderReadDto(o));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetByIDAsync(id);
        if (order == null) return NotFound();
        return Ok(new OrderReadDto(order));
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateDto dto)
    {
        try
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderStatus = new OrderStatus { Name = dto.OrderStatusName }
            };

            var created = await _orderService.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new OrderReadDto(created));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest(new { message = "ID mismatch" });

        try
        {
            var order = new Order
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                OrderStatus = new OrderStatus { Name = dto.OrderStatusName }
            };

            var updated = await _orderService.UpdateAsync(order);
            return Ok(new OrderReadDto(updated));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _orderService.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = "Order not found" });
        return Ok(new { message = "Order deleted successfully" });
    }
}

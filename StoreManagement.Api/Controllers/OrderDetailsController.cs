using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Models;
using StoreManagement.Api.DTOs;
using System.Linq;

namespace StoreManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController : ControllerBase
{
    private readonly IOrderDetailsService _orderDetailsService;

    public OrderDetailsController(IOrderDetailsService orderDetailsService)
    {
        _orderDetailsService = orderDetailsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var details = await _orderDetailsService.GetAllAsync();
        var dtos = details.Select(d => new OrderDetailsReadDto(d));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var detail = await _orderDetailsService.GetByIdAsync(id);
        if (detail == null) return NotFound();
        return Ok(new OrderDetailsReadDto(detail));
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderDetailsCreateDto dto)
    {
        var detail = dto.ToOrderDetails();
        var created = await _orderDetailsService.CreateAsync(detail);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new OrderDetailsReadDto(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderDetailsUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var detail = dto.ToOrderDetails();
        var updated = await _orderDetailsService.UpdateAsync(detail);
        return Ok(new OrderDetailsReadDto(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _orderDetailsService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return Ok(new { message = "Order detail deleted successfully" });
    }
}
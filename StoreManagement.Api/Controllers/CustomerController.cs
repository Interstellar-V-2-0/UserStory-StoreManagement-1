using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Models;
using StoreManagement.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace StoreManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        var dtos = customers.Select(c => new CustomerReadDto(c));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetByIDAsync(id);
        if (customer == null) return NotFound();
        return Ok(new CustomerReadDto(customer));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateDto dto)
    {
        var customer = dto.ToCustomer();
        var created = await _customerService.CreateAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new CustomerReadDto(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CustomerUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var customer = dto.ToCustomer();
        var updated = await _customerService.UpdateAsync(customer);
        return Ok(new CustomerReadDto(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _customerService.GetByIDAsync(id);
        if (customer == null) return NotFound();
        var deleted = await _customerService.DeleteAsync(id);
        return Ok(new CustomerReadDto(deleted));
    }
}
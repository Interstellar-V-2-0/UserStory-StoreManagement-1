using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Models;
using StoreManagement.Api.DTOs;
using System.Linq;

namespace StoreManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        var dtos = products.Select(p => new ProductReadDto(p));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIDAsync(id);
        if (product == null) return NotFound();
        return Ok(new ProductReadDto(product));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        var product = dto.ToProduct();
        var created = await _productService.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ProductReadDto(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var product = dto.ToProduct();
        var updated = await _productService.UpdateAsync(product);
        return Ok(new ProductReadDto(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetByIDAsync(id);
        if (product == null) return NotFound();
        var deleted = await _productService.DeleteAsync(id);
        return Ok(new ProductReadDto(deleted));
    }
}
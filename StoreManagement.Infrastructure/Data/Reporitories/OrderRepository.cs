using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Infrastructure.Data.Reporitories;

public class OrderRepository : IOrderRepository<Order>
{
    
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int? id)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderStatus)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> CustomerExistAsync(int? id)
    {
        return await _context.Customers.AnyAsync(c => c.Id == id);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(int? customerId)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderStatus)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }
    
}
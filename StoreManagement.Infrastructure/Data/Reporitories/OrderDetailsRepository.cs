using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Infrastructure.Data.Reporitories;

public class OrderDetailsRepository : IOrderDetailsRepository<OrderDetails>
{
    private readonly AppDbContext _context;
    
    public OrderDetailsRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<OrderDetails>> GetAllAsync()
    {
        return await  _context.OrderDetails
            .Include(od => od.Order)
            .Include(od => od.Product)
            .ToListAsync();
    }

    public async Task<OrderDetails?> GetByIdAsync(int id)
    {
        return await _context.OrderDetails
            .Include(od => od.Order)
            .Include(od => od.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(od => od.Id == id);
    }

    public async Task<IEnumerable<OrderDetails>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderDetails
            .Include(od => od.Order)
            .Include(od => od.Product)
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<OrderDetails> AddAsync(OrderDetails orderDetails)
    {
        _context.OrderDetails.Add(orderDetails);
        await _context.SaveChangesAsync();
        return orderDetails;
    }

    public async Task<OrderDetails> UpdateAsync(OrderDetails orderDetails)
    {
        _context.OrderDetails.Update(orderDetails);
        await _context.SaveChangesAsync();
        return orderDetails;
    }

    public async Task<OrderDetails> DeleteAsync(OrderDetails orderDetails)
    {
        _context.OrderDetails.Remove(orderDetails);
        await _context.SaveChangesAsync();
        return orderDetails;
    }
}
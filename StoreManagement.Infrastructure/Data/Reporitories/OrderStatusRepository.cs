using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Infrastructure.Data.Reporitories;

public class OrderStatusRepository : IOrderStatusRepository<OrderStatus>
{
    private readonly AppDbContext _context;

    public OrderStatusRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<OrderStatus>> GetAllAsync()
    {
        return await _context.OrderStatuses.ToListAsync();
    }

    public async Task<OrderStatus?> GetByIDAsync(int? orderStatusId)
    {
        return await _context.OrderStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(os => os.Id == orderStatusId);
    }
}
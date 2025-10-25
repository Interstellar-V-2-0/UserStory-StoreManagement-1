using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Interfaces;

public interface IOrderDetailsService
{
    Task<IEnumerable<OrderDetails>> GetAllAsync();
    Task<OrderDetails> GetByIdAsync(int id);
    Task<OrderDetails> CreateAsync(OrderDetails orderDetails);
    Task<OrderDetails> UpdateAsync(OrderDetails orderDetails);
    Task<bool> DeleteAsync(int id);
}
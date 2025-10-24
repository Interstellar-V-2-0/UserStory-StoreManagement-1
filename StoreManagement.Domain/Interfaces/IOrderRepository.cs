using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.Interfaces;

public interface IOrderRepository<T> where T : Order
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int? id);
    Task<T> CreateAsync(T order);
    Task<T> UpdateAsync(T order);
    Task<T> DeleteAsync(T order);
    
    Task<bool> CustomerExistAsync(int? id);
    Task<List<Order>> GetByCustomerIdAsync(int? customerId);

}
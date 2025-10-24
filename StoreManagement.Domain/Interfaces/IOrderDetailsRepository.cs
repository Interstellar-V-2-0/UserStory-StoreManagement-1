using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.Interfaces;

public interface IOrderDetailsRepository<T> where T : OrderDetails
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetByOrderIdAsync(int orderId);
    Task<T> AddAsync(T orderDetails);
    Task<T> UpdateAsync(T orderDetails);
    Task<T> DeleteAsync(T orderDetails);
    
}
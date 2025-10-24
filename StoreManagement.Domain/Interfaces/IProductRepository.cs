using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.Interfaces;

public interface IProductRepository<T> where T : Product
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIDAsync(int? productId);
    Task<T> CreateAsync(T product);
    Task<T> UpdateAsync(T product);
    Task<T> DeleteAsync(T product);
    
}
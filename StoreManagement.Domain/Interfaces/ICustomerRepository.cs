using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.Interfaces;

public interface ICustomerRepository<T> where T : Customer
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIDAsync(int? customerId);
    Task<T> CreateAsync(Customer customer);
    Task<T> UpdateAsync(Customer customer);
    Task<T> DeleteAsync(Customer customer);
    
}
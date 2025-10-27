using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIDAsync(int id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task<Customer> DeleteAsync(int id);
}
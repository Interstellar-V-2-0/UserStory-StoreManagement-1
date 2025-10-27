using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository<Customer> _customerRepository;

    public CustomerService(ICustomerRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer> GetByIDAsync(int id)
    {
        return await _customerRepository.GetByIDAsync(id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        return await _customerRepository.CreateAsync(customer);
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        return await _customerRepository.UpdateAsync(customer);
    }

    public async Task<Customer> DeleteAsync(int id)
    {
        var customer = await _customerRepository.GetByIDAsync(id);
        return await _customerRepository.DeleteAsync(customer);
    }
}
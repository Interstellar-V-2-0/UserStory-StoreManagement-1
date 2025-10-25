using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Service
{
    public class CustomerService 
    {
        private readonly ICustomerRepository<Customer> _repo;
        private ICustomerRepository<Customer> _customerRepositoryImplementation;

        public CustomerService(ICustomerRepository<Customer> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _repo.GetByIDAsync(id);
        }

        public async Task<Customer> Create(Customer customer)
        {
            if (customer.Email == null || customer.Email.Trim() == "")
                throw new Exception("El email no puede estar vacío");

            return await _repo.CreateAsync(customer);
        }

        public async Task Update(Customer customer)
        {
            if (customer.Email == null || customer.Email.Trim() == "")
                throw new Exception("El email no puede estar vacío");

            await _repo.UpdateAsync(customer);
        }

        public async Task Delete(Customer id)
        {
            await _repo.DeleteAsync(id);
        }
    }
} 

using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Infrastructure.Data.Reporitories;

public class CustomerRepository : ICustomerRepository<Customer>
{
    private readonly AppDbContext _context;
    
    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIDAsync(int? customerId)
    {
        return await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == customerId);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
}
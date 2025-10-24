using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Models;

namespace StoreManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options){}
    
    public AppDbContext() { }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
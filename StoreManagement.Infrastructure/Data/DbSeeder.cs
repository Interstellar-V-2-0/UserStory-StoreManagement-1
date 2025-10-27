using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;

namespace StoreManagement.Infrastructure.Seeders
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Asegurarnos que la base de datos esté creada
            context.Database.EnsureCreated();

            // Solo insertar si no hay datos
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Chocolate Cake", Description = "Delicious dark chocolate cake", Price = 25.5M },
                    new Product { Name = "Vanilla Cupcake", Description = "Soft vanilla cupcake", Price = 5.0M },
                    new Product { Name = "Strawberry Tart", Description = "Fresh strawberries tart", Price = 12.0M }
                };

                context.Products.AddRange(products);
            }

            if (!context.Customers.Any())
            {
                var customers = new List<Customer>
                {
                    new Customer { FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com" },
                    new Customer { FirstName = "Bob", LastName = "Smith", Email = "bob@example.com" },
                    new Customer { FirstName = "Charlie", LastName = "Brown", Email = "charlie@example.com" }
                };

                context.Customers.AddRange(customers);
            }

            context.SaveChanges();
        }
    }
}
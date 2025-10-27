using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Interfaces;
using StoreManagement.Application.Services;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;
using StoreManagement.Infrastructure.Data;
using StoreManagement.Infrastructure.Data.Reporitories;
using StoreManagement.Infrastructure.Seeders; // Seeder

var builder = WebApplication.CreateBuilder(args);

// ===== Cadena de conexión =====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ===== DbContext con MySQL =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ===== Controllers y Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Repositories =====
builder.Services.AddScoped<ICustomerRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IProductRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IOrderRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IOrderDetailsRepository<OrderDetails>, OrderDetailsRepository>();
builder.Services.AddScoped<IOrderStatusRepository<OrderStatus>, OrderStatusRepository>();

// ===== Services =====
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();

var app = builder.Build();

// ===== Swagger solo en desarrollo =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ===== Seed de datos de prueba =====
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(dbContext); // Aquí se queman Customers, Products, Orders, etc.
}

// ===== Ejecuta la app =====
app.Run();

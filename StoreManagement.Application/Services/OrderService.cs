using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository<Order> _orderRepository;
    private readonly ICustomerRepository<Customer> _customerRepository;

    // Lista de estados válidos hardcodeados
    public static readonly string[] DefaultStatuses = { "Pending", "Completed", "Cancelled", "Shipped" };

    public OrderService(
        IOrderRepository<Order> orderRepository,
        ICustomerRepository<Customer> customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order> GetByIDAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        // validar cliente
        bool customerExists = await _orderRepository.CustomerExistAsync(order.CustomerId);
        if (!customerExists)
            throw new Exception("Cliente no existente");

        // validar estado usando lista hardcodeada
        if (order.OrderStatus == null || !DefaultStatuses.Contains(order.OrderStatus.Name))
            throw new Exception("Estado de pedido inválido");

        order.OrderDate = DateTime.UtcNow;

        return await _orderRepository.CreateAsync(order);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var existing = await _orderRepository.GetByIdAsync(order.Id);
        if (existing == null)
            throw new Exception("No se encontró el pedido para actualizar");

        // validar cliente
        bool customerExists = await _orderRepository.CustomerExistAsync(order.CustomerId);
        if (!customerExists)
            throw new Exception("Cliente inválido");

        // validar estado
        if (order.OrderStatus == null || !DefaultStatuses.Contains(order.OrderStatus.Name))
            throw new Exception("Estado de pedido inválido");

        return await _orderRepository.UpdateAsync(order);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return false;

        await _orderRepository.DeleteAsync(order);
        return true;
    }
}

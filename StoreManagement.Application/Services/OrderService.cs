using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;



namespace StoreManagement.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository<Order> _orderRepository;
    private readonly IOrderStatusRepository<OrderStatus> _orderStatusRepository;
    private readonly ICustomerRepository<Customer> _customerRepository;

    public OrderService(
        IOrderRepository<Order> orderRepository,
        IOrderStatusRepository<OrderStatus> orderStatusRepository,
        ICustomerRepository<Customer> customerRepository)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
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
        //validar cliente
        bool customerExists = await _orderRepository.CustomerExistAsync(order.CustomerId);
        if (!customerExists)
            throw new Exception("cliente no existente");
        
        //validar estado pedido
        var validStatus = await _orderRepository.GetByIdAsync(order.OrderStatusId);
        if (validStatus == null)
            throw new Exception("estado del pedido invalido");

        order.OrderDate = DateTime.UtcNow;

        return await _orderRepository.CreateAsync(order);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var existing = await _orderRepository.GetByIdAsync(order.Id);
        if (existing == null)
            throw new Exception("no se encontro el pedido para actualizar");
        
        //validar cliente si cambia
        bool customerExists = await _orderRepository.CustomerExistAsync(order.CustomerId);
        if (!customerExists)
            throw new Exception("cliente invalido");
        
        //validar estado
        var validStatus = await _orderStatusRepository.GetByIDAsync(order.OrderStatusId);
        if (validStatus == null)
            throw new Exception("estado de pedido invalido");

        return await _orderRepository.UpdateAsync(order);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return false;

        await _orderRepository.DeleteAsync(order);
        return true;
    }
}
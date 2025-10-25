using StoreManagement.Application.Interfaces;
using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Services;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly IOrderDetailsRepository<OrderDetails> _orderDetailsRepository;
    private readonly IOrderRepository<Order> _orderRepository;
    private readonly IProductRepository<Product> _productRepository;
    
    public OrderDetailsService(
        IOrderDetailsRepository<OrderDetails> orderDetailsRepository,
        IOrderRepository<Order> orderRepository,
        IProductRepository<Product> productRepository)
    {
        _orderDetailsRepository = orderDetailsRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<OrderDetails>> GetAllAsync()
    {
        return await _orderDetailsRepository.GetAllAsync();
    }

    public async Task<OrderDetails> GetByIdAsync(int id)
    {
        return await _orderDetailsRepository.GetByIdAsync(id);
    }

    public async Task<OrderDetails> CreateAsync(OrderDetails orderDetails)
    {
        //validar pedido 
        var order = await _orderRepository.GetByIdAsync(orderDetails.OrderId);
        if (order == null)
            throw new Exception("No se puede agregar el detalle el pedido no existe");
        
        //valir producto existe
        var product = await _productRepository.GetByIDAsync(orderDetails.ProductId);
        if (product == null)
            throw new Exception("No se puede agregar el producto no existe");
        
        return await _orderDetailsRepository.AddAsync(orderDetails);
    }

    public async Task<OrderDetails> UpdateAsync(OrderDetails orderDetails)
    {
        var existing = await _orderDetailsRepository.GetByIdAsync(orderDetails.Id);
        if (existing == null)
            throw new Exception("no se encontroel detalle del pedido para actualizar");
        
        return await _orderDetailsRepository.UpdateAsync(orderDetails);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _orderDetailsRepository.GetByIdAsync(id);
        if (existing == null)
            return false;

        await _orderDetailsRepository.DeleteAsync(existing);
        return true;
    }
}
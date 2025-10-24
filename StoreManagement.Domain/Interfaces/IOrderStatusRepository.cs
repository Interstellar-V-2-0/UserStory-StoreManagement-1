using StoreManagement.Domain.Models;

namespace StoreManagement.Domain.Interfaces;

public interface IOrderStatusRepository<T> where T : OrderStatus
{
    Task<IEnumerable<OrderStatus>> GetAllAsync();
    Task<T> GetByIDAsync(int? orderStatusId);
}
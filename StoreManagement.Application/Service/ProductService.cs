using StoreManagement.Domain.Interfaces;
using StoreManagement.Domain.Models;

namespace StoreManagement.Application.Service
{
    public class ProductService 
    {
        private readonly IProductRepository<Product> _repo;

        public ProductService(IProductRepository<Product> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _repo.GetByIDAsync(id);
        }

        public async Task<Product> Create(Product product)
        {
            if (product.Price <= 0)
                throw new ArgumentException("El precio debe ser mayor que 0.");

            return await _repo.CreateAsync(product);
        }

        public async Task Update(Product product)
        {
            if (product.Price <= 0)
                throw new ArgumentException("El precio debe ser mayor que 0.");

            await _repo.UpdateAsync(product);
        }

        public async Task Delete(Product id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
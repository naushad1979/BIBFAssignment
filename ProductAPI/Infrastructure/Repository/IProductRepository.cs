using ProductAPI.Infrastructure.Entity;
using ProductAPI.Infrastructure.Base.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAPI.Infrastructure.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<int> CreateProductAsync(Product product);       
        public Task<int> UpdateProductAsync(Product product);
        public Task<int> DeleteProductAsync(int productId);
        public Task<Product> GetProductAsync(int productId);
        public Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}

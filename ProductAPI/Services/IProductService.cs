using AccountAPI.Models;
using AccountAPI.Response;
using ProductAPI.Infrastructure.Entity;

namespace AccountAPI.Services
{
    public interface IProductService
    {
        public Task<ProductResponse> CreateProductAsync(ProductModel product);       
        public Task<ProductResponse> UpdateProductAsync(ProductModel product);
        public Task<ProductResponse> DeleteProductAsync(int productId);
        public Task<ProductModel> GetProductByIdAsync(int productId);
        public Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    }
}

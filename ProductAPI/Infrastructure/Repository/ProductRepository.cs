using Account.API.Infrastructure;
using AccountAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Infrastructure.Entity;
using ProductAPI.Infrastructure.Base.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            int result = 0;            
            var exists = ExistsAsync(product.Id).Result;
            result = (exists ? -1 : await SaveAsync(product));             
            
            return result;
        }

        public async Task<int> DeleteProductAsync(int productId)
        {
            int result = 0;
            var productExists = await FindByIdAsync(productId);
            result = (productExists ==null? -1 : await DeleteAsync(productExists));

            return result;             
        }       

        public async Task<int> UpdateProductAsync(Product product)
        {
            int result = 0;
            var productExists = await FindByIdAsync(product.Id);
            

            if(productExists != null)
            {
                productExists.Id = product.Id;
                productExists.Description = product.Description;
                productExists.Name = product.Name;
                productExists.Price = product.Price;
                productExists.UpdatedBy = "Admin";
                productExists.UpdatedDate = DateTime.Now.ToUniversalTime();
            }

            result = (productExists == null ? -1 : await UpdateAsync(productExists));

            return result;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await FindAllAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await FindByIdAsync(productId);
        }
    }
}

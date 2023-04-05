using AccountAPI.Models;
using AccountAPI.Response;
using AccountAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Exceptions;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        #region Private Variables
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        #endregion

        #region Constructor
        public ProductController(ILogger<ProductController> logger
            , IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        #endregion

        #region Create,Update & Delete methods
        [HttpPost]       
        public async Task<ActionResult<ProductResponse>> CreateAsync(ProductModel request)
        {
            _logger.LogInformation("This is a log message. This is an object: {ProductModel}", new { name = "John Doe" });
            if (request == null)
            {
                return BadRequest("Product Details Invalid");
            }
            var apiResponse = await _productService.CreateProductAsync(request);
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteAsync(int id)
        {
            if (id < -1)
            {
                return BadRequest("Invalid Product Id. It should be greater than zero");
            }
            var apiResponse = await _productService.DeleteProductAsync(id);
            return Ok(apiResponse);
        }

        [HttpPut]       
        public async Task<ActionResult<ProductResponse>> UpdateAsync(ProductModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Product. It should not null");
            }
            var apiResponse = await _productService.UpdateProductAsync(request);
            return Ok(apiResponse);
        }
        #endregion

        #region Get methods 
        [HttpGet]        
        public async Task<ActionResult<ProductModel>> GetByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllAsync()
        {
            _logger.LogInformation($": Call to GetAllAsync() start");

            var products = await _productService.GetAllProductsAsync();

            _logger.LogInformation($": Call to GetAllAsync() end");
            return Ok(products);
        }
        #endregion
    }
}
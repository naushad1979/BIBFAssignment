using AccountAPI.Models;
using AccountAPI.Response;
using AccountAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            _logger.LogInformation($"ProductController=>CreateAsync initiated with request {JsonSerializer.Serialize(request)}");
           
            if (request == null)
            {                
                return BadRequest("Product Details Invalid");
            }
            var apiResponse = await _productService.CreateProductAsync(request);
            
            _logger.LogInformation($"ProductController=>CreateAsync executed successfully with response: {JsonSerializer.Serialize(apiResponse)}");
            
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteAsync(int id)
        {
            _logger.LogInformation($"ProductController=>DeleteAsync initiated with id {id}");
            if (id < -1)
            {
                return BadRequest("Invalid Product Id. It should be greater than zero");
            }
            var apiResponse = await _productService.DeleteProductAsync(id);

            _logger.LogInformation($"ProductController=>DeleteAsync executed successfully with response: {JsonSerializer.Serialize(apiResponse)}");
            return Ok(apiResponse);
        }

        [HttpPut]       
        public async Task<ActionResult<ProductResponse>> UpdateAsync(ProductModel request)
        {
            _logger.LogInformation($"ProductController=>UpdateAsync initiated with request {JsonSerializer.Serialize(request)}");
            if (request == null)
            {
                return BadRequest("Invalid Product. It should not null");
            }
            var apiResponse = await _productService.UpdateProductAsync(request);

            _logger.LogInformation($"ProductController=>UpdateAsync executed successfully with response: {JsonSerializer.Serialize(apiResponse)}");
            return Ok(apiResponse);
        }
        #endregion

        #region Get methods 
        [HttpGet]        
        public async Task<ActionResult<ProductModel>> GetByIdAsync(int id)
        {
            _logger.LogInformation($"ProductController=>GetByIdAsync initiated with id {id}");
            
            var product = await _productService.GetProductByIdAsync(id);
            
            _logger.LogInformation($"ProductController=>GetByIdAsync executed successfully with response: {JsonSerializer.Serialize(product)}");
            return Ok(product);
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllAsync()
        {
            _logger.LogInformation($"ProductController=>GetAllAsync initiated");

            var products = await _productService.GetAllProductsAsync();

            _logger.LogInformation($"ProductController=>GetAllAsync executed successfully with response: {JsonSerializer.Serialize(products)}");
            return Ok(products);
        }
        #endregion
    }
}
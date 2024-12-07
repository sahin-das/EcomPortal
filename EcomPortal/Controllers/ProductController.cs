using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Product;
using EcomPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IGenericService<Product, AddProductDto, UpdateProductDto> productService, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IGenericService<Product, AddProductDto, UpdateProductDto> _productService = productService;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Executing Get method");
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productService.CreateAsync(request);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _productService.UpdateAsync(id, request);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Ok("Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

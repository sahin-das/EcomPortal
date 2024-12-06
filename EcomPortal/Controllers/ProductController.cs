using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Product;
using EcomPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IGenericService<Product, AddProductDto, UpdateProductDto> ProductService, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IGenericService<Product, AddProductDto, UpdateProductDto> _ProductService = ProductService;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Executing Get method");
            var Products = await _ProductService.GetAllAsync();
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var Product = await _ProductService.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(Product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Product = await _ProductService.CreateAsync(request);
            return Ok(Product);
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
                var Product = await _ProductService.UpdateAsync(id, request);
                return Ok(Product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _ProductService.DeleteAsync(id);
                return Ok("Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

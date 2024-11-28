using EcomPortal.Data;
using EcomPortal.Models;
using EcomPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ApplicationDbContext dbContext) : ControllerBase
    {
        public readonly ApplicationDbContext dbContext = dbContext;

        [HttpGet]
        public IActionResult GetProduct()
        {
            var products = dbContext.Products.ToList();
            if (products != null)
            {
                throw new Exception("Sahin Errror in Logger");
            }
            
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var product = new Product()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Category = addProductDto.Category,
                Price = addProductDto.Price
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return Ok(product);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Category = updateProductDto.Category;
            product.Price = updateProductDto.Price;
            dbContext.SaveChanges();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(product);
            return Ok(product);
        }
    }
}

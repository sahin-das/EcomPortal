using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Product;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class ProductService(IGenericRepository<Product> productRepository) : 
        GenericService<Product, AddProductDto, UpdateProductDto>(productRepository),
        IGenericService<Product, AddProductDto, UpdateProductDto>
    {
        private readonly IGenericRepository<Product> _productRepository = productRepository;

        public override Product MapToEntity(AddProductDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            var product = new Product()
            {
                Name = dto.Name,
                Price = dto.Price,
                Category = dto.Category,
                Description= dto.Description
            };

            return product;
        }

        public override void MapToEntity(UpdateProductDto dto, Product entity)
        {
            ArgumentNullException.ThrowIfNull(dto);
            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.Category = dto.Category;
            entity.Description = dto.Description;
        }
    }
}

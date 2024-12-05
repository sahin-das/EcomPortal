using EcomPortal.Models.Entities;
using EcomPortal.Models.ProductDto;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class ProductService(IGenericRepository<Product> ProductRepository) : 
        GenericService<Product, AddProductDto, UpdateProductDto>(ProductRepository),
        IGenericService<Product, AddProductDto, UpdateProductDto>
    {
        private readonly IGenericRepository<Product> _ProductRepository = ProductRepository;

        public override Product MapToEntity(AddProductDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            var Product = new Product()
            {
                Name = dto.Name,
                Price = dto.Price,
                Category = dto.Category,
                Description= dto.Description
            };

            return Product;
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

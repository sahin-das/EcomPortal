using EcomPortal.Models;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : CrudRepository<Product>(context), ICrudRepository<Product>
    {
    }
}

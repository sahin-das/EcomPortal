using EcomPortal.Data;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IGenericRepository<Product>
    {
    }
}

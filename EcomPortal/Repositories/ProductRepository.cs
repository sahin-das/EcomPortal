using EcomPortal.Data;
using EcomPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcomPortal.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IGenericRepository<Product>
    {
        private readonly ApplicationDbContext _context = context;
    }
}

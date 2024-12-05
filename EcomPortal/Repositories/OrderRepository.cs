using EcomPortal.Data;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IGenericRepository<Order>
    {
        private readonly ApplicationDbContext _context = context;
    }
}

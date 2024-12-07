using EcomPortal.Data;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class OrderProductRepository(ApplicationDbContext context) : GenericRepository<Order>(context)
    {
    }
}

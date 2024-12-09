using EcomPortal.Models;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class OrderProductRepository(ApplicationDbContext context) : CrudRepository<Order>(context)
    {
    }
}

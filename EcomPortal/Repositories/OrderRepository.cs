using EcomPortal.Data;
using EcomPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcomPortal.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IGenericRepository<Order>
    {
        private readonly ApplicationDbContext _context = context;
    }
}

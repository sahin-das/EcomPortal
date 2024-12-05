using EcomPortal.Data;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IGenericRepository<User>
    {
        private readonly ApplicationDbContext _context = context;
    }
}

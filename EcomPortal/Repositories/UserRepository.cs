using EcomPortal.Models;
using EcomPortal.Models.Entities;

namespace EcomPortal.Repositories
{
    public class UserRepository(ApplicationDbContext context) : CrudRepository<User>(context), ICrudRepository<User>
    {
    }
}

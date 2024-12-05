using EcomPortal.Models.Entities;
using EcomPortal.Models.UserDto;

namespace EcomPortal.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task CreateUserAsync(AddUserDto request);
        Task UpdateUserAsync(Guid id, AddUserDto request);
        Task DeleteUserAsync(Guid id);
    }
}

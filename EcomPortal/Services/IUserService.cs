using EcomPortal.Models.Entities;
using EcomPortal.Models.UserDto;

namespace EcomPortal.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(AddUserDto request);
        Task<User> UpdateUserAsync(Guid id, UpdateUserDto request);
        Task DeleteUserAsync(Guid id);
    }
}

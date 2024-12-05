using EcomPortal.Models.Entities;
using EcomPortal.Models.UserDto;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        private readonly IUserRepository _repository = repository;

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(AddUserDto request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email
            };

            await _repository.AddAsync(user);
        }

        public async Task UpdateUserAsync(Guid id, AddUserDto request)
        {
            var user = await _repository.GetByIdAsync(id) ?? throw new Exception("User not found");
            user.Name = request.Name;
            user.Email = request.Email;

            await _repository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

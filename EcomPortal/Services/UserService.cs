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

        public async Task<User> CreateUserAsync(AddUserDto request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            var userCreated = await _repository.AddAsync(user);
            return userCreated;
        }

        public async Task<User> UpdateUserAsync(Guid id, UpdateUserDto request)
        {
            var user = await _repository.GetByIdAsync(id) ?? throw new Exception("User not found");
            user.Name = request.Name;
            user.Email = request.Email;

            var userUpdated = await _repository.UpdateAsync(user);
            return userUpdated;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

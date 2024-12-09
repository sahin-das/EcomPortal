using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.User;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class UserService(ICrudRepository<User> userRepository)
    {
        private readonly ICrudRepository<User> _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(AddUserDto request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                CreatedDate = DateTime.UtcNow
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateAsync(Guid id, UpdateUserDto request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await _userRepository.GetByIdAsync(id) ??
                throw new KeyNotFoundException($"User with ID {id} not found.");
            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.UpdatedDate = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}

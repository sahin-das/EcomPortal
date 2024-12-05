using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.User;
using EcomPortal.Repositories;

namespace EcomPortal.Services
{
    public class UserService(IGenericRepository<User> userRepository) : 
        GenericService<User, AddUserDto, UpdateUserDto>(userRepository), 
        IGenericService<User, AddUserDto, UpdateUserDto>
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;

        public override User MapToEntity(AddUserDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            var user = new User() 
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
            };

            return user;
        }

        public override void MapToEntity(UpdateUserDto dto, User entity)
        {
            ArgumentNullException.ThrowIfNull(dto);
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Phone = dto.Phone;
        }
    }
}

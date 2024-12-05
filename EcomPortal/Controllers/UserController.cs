using EcomPortal.Models.Entities;
using EcomPortal.Models.UserDto;
using EcomPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IGenericService<User, AddUserDto, UpdateUserDto> userService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IGenericService<User, AddUserDto, UpdateUserDto> _userService = userService;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Executing Get method");
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.CreateAsync(request);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.UpdateAsync(id, request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok("Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

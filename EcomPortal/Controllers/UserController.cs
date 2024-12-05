﻿using EcomPortal.Models.UserDto;
using EcomPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Executing Get method");
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
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
            await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = request.Name }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] AddUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.UpdateUserAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}

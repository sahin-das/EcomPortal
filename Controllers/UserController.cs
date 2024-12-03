using EcomPortal.Data;
using EcomPortal.Models;
using EcomPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ApplicationDbContext dbContext, ILogger<UserController> logger) : ControllerBase
    {
        public readonly ApplicationDbContext dbContext = dbContext;
        private readonly ILogger<UserController> _logger = logger;

        [HttpGet]
        public IActionResult GetUser()
        {
            _logger.LogInformation("Executing Get method");
            var Users = dbContext.Users.ToList();
            if (Users == null)
            {
                _logger.LogError("sahin log: No Users found.");
                return NotFound("No Users found.");
            }

            return Ok(Users);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetUserById(Guid id)
        {
            var User = dbContext.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
            _logger.LogInformation("Executing POST method: " + addUserDto);
            var User = new User()
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Phone = addUserDto.Phone
            };
            dbContext.Users.Add(User);
            dbContext.SaveChanges();
            return Ok(User);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var User = dbContext.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            User.Name = updateUserDto.Name;
            User.Phone = updateUserDto.Phone;
            User.Email = updateUserDto.Email;
            dbContext.SaveChanges();
            return Ok(User);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            var User = dbContext.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(User);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}

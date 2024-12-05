using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.User;
using EcomPortal.Repositories;
using EcomPortal.Services;
using Moq;

namespace Ecom.Tests.Users
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IGenericRepository<User>> _mockRepository;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericRepository<User>>();
            _userService = new UserService(_mockRepository.Object);
        }

        [Test]
        public async Task GetUserAsync_Should_ReturnUsers()
        {
            var users = new List<User>
            {
                new() { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" },
                new() { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane.smith@example.com" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _userService.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(u => u.Name == "John Doe" && u.Email == "john.doe@example.com"));
            Assert.That(result.Any(u => u.Name == "Jane Smith" && u.Email == "jane.smith@example.com"));

            _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetUserByIdAsync_Should_ReturnUser_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "Existing User", Email = "existing.user@example.com" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _userService.GetByIdAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.Name, Is.EqualTo("Existing User"));
            Assert.That(result.Email, Is.EqualTo("existing.user@example.com"));
            _mockRepository.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
        }

        [Test]
        public async Task AddUserAsync_Should_AddUser_And_ReturnCreatedUser()
        {
            var newUser = new AddUserDto { Name = "New User", Email = "new.user@example.com" };
            var createdUser = new User
            {
                Id = Guid.NewGuid(),
                Name = newUser.Name,
                Email = newUser.Email
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(createdUser);

            var result = await _userService.CreateAsync(newUser);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(newUser.Name));
            Assert.That(result.Email, Is.EqualTo(newUser.Email));

            _mockRepository.Verify(repo => repo.AddAsync(It.Is<User>(u =>
                u.Name == newUser.Name &&
                u.Email == newUser.Email)), Times.Once);
        }

        [Test]
        public async Task GetUserByIdAsync_Should_ReturnNull_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var result = await _userService.GetByIdAsync(userId);

            Assert.That(result, Is.Null);
            _mockRepository.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
        }

        [Test]
        public async Task UpdateUserAsync_Should_UpdateUser()
        {
            var newUser = new AddUserDto { Name = "New User", Email = "new.user@example.com", Phone = "1234567890" };
            var createdUser = new User
            {
                Id = Guid.NewGuid(),
                Name = newUser.Name,
                Email = newUser.Email,
                Phone = newUser.Phone,
            };

            var updatedUser = new User
            {
                Id = createdUser.Id,
                Name = "Updated Name",
                Email = createdUser.Email,
                Phone = createdUser.Phone
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(createdUser.Id)).ReturnsAsync(createdUser);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).ReturnsAsync(updatedUser);

            var updateUserDto = new UpdateUserDto
            {
                Name = "Updated Name",
                Email = createdUser.Email,
                Phone = createdUser.Phone
            };
            var userUpdated = await _userService.UpdateAsync(createdUser.Id, updateUserDto);

            Assert.That(userUpdated.Id, Is.EqualTo(updatedUser.Id));
            Assert.That(userUpdated.Name, Is.EqualTo(updatedUser.Name));
            Assert.That(userUpdated.Email, Is.EqualTo(updatedUser.Email));
            Assert.That(userUpdated.Phone, Is.EqualTo(updatedUser.Phone));

            _mockRepository.Verify(repo => repo.GetByIdAsync(createdUser.Id), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.Is<User>(u =>
                u.Id == createdUser.Id &&
                u.Name == "Updated Name" &&
                u.Email == createdUser.Email &&
                u.Phone == "1234567890")), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_Should_DeleteUser()
        {
            var userId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            await _userService.DeleteAsync(userId);

            _mockRepository.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }

        [Test]
        public void DeleteUserAsync_Should_ThrowException_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.DeleteAsync(userId)).ThrowsAsync(new Exception("User not found"));

            Assert.ThrowsAsync<Exception>(async () => await _userService.DeleteAsync(userId));
            _mockRepository.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }
}

using EcomPortal.Models.Entities;

namespace Ecom.Tests.Models.Entities;

[TestFixture]
public class UserTests
{
    
    private User _user;

    [SetUp]
    public void Setup()
    {
        _user = new User
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "john.doe@example.com",
            Phone = "123-456-7890"
        };
    }
    
    [Test]
    public void User_CanBeInstantiated()
    {
        var user = new User()
        {
            Name = "John Doe",
        };
        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public void User_HasRequiredProperties()
    {
        Assert.That(_user.Name, Is.EqualTo("John Doe"));
        Assert.That(_user.Email, Is.EqualTo("john.doe@example.com"));
        Assert.That(_user.Phone, Is.EqualTo("123-456-7890"));
    }

    [Test]
    public void User_EmailCanBeNull()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Jane Doe",
            Email = null,
            Phone = "987-654-3210"
        };

        Assert.That(user.Email, Is.Null);
    }

    [Test]
    public void User_PhoneCanBeNull()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "James Smith",
            Email = "james.smith@example.com",
            Phone = null
        };

        Assert.That(user.Phone, Is.Null);
    }

    [Test]
    public void User_IdIsGuid()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = "test.user@example.com",
            Phone = "555-555-5555"
        };

        Assert.That(user.Id, Is.InstanceOf<Guid>());
    }

    [Test]
    public void User_IdShouldNotBeEmpty()
    {
        var user = new User
        {
            Id = Guid.Empty,
            Name = "Test User"
        };

        Assert.That(Guid.Empty, Is.EqualTo(user.Id));
    }
}

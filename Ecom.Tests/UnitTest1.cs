using EcomPortal.Models.Entities;

namespace Ecom.Tests;

public class Tests
{
    private User _user;
    private Product _product;
    
    [SetUp]
    public void Setup()
    {
        _user = new User()
        {
            Name = "John",
            Email = "john@gmail.com",
            Phone = "123456789",
        };
        _product = new Product()
        {
            Name = "Apple",
            Price = 1.99M,
            Category = "Mobile",
            Description = "Phone to make calls"
        };
    }

    [Test]
    public void Test1()
    {
        Assert.That(_user, Is.Not.Null);
        Assert.That(_product, Is.Not.Null);
    }
}
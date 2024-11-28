using EcomPortal.Models.Entities;
using NUnit.Framework;


namespace EcomPortal.Tests
{
    [TestFixture]
    public class TestClass
    {
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _product = new Product()
            {
                Name = "TestName",
                Description = "TestDesc",
                Category = "TestCat",
                Price = 1,
            };
        }

        [Test]
        public void UpdatePriceTest()
        {
            var before = _product.Price;
            _product.Price += 1;
            var after = _product.Price;
            Assert.Equals(before, after);
        }
    }
}

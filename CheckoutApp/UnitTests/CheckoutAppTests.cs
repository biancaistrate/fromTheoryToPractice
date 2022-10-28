using AutoMapper;
using CheckoutApp.Controllers;
using CheckoutApp.Data;
using CheckoutApp.DataModels;
using CheckoutApp.Models;
using CheckoutApp.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    [TestClass]
    public class CheckoutAppTests
    {
        [TestMethod]
        public void TestBasketCreation()
        {
            var basket = new Basket() { Customer = "Edi" };
            Assert.AreEqual(0, basket.TotalNet);
            Assert.AreEqual(0, basket.TotalGross);

            basket.AddArticle(new Article("apple", 3));
            basket.AddArticle(new Article("orage", 4));

            Assert.AreEqual(7, basket.TotalNet);
            Assert.AreEqual(7.7, basket.TotalGross);

            Assert.AreEqual(Status.Open, basket.Status);
            Assert.AreEqual("Edi", basket.Customer);
        }
        [TestMethod]
        public void TestBasketsCreation()
        {
            var basket1 = new Basket() { Customer = "Mike", Id = 1 };
            var basket2 = new Basket() { Customer = "Emma", Id = 2 };
            Assert.AreNotEqual(basket1.Id, basket2.Id);
            Assert.AreNotEqual(Status.Closed, basket1.Status);

        }

        [TestMethod]
        public async Task TestCreateBasketRequest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<BasketProfile>());
            var mapper = new Mapper(config);

            var controller = new BasketsController(new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase("BasketsDb").Options), mapper);
            
            // Act
            var response = await controller.Create(new CustomerInfo
            {
                Name = "test",
                PaysVAT = true
            }, CancellationToken.None) ;

            var r = GetObjectResultContent<BasketDTO>(response?.Result);
            Assert.IsNotNull(r?.Id);
            Assert.AreEqual("test", r?.Customer);
            Assert.AreEqual(true, r?.PaysVAT);
            Assert.AreEqual(0, r?.TotalNet);
            Assert.AreEqual(0, r?.TotalGross);
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
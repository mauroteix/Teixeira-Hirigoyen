using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MSP.BetterCalm.APITest
{
    [TestClass]
    public class CategoryControllerTest
    {
        List<Category> categoryList;

        [TestInitialize]
        public void Initialize()
        {
            Category category = new Category()
            {
                Id = 1,
                Name = "Dormir",
            };
            categoryList = new List<Category>();
            categoryList.Add(category);
        }
        [TestMethod]
        public void GetAllCategories()
        {
            var mockUser = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockUser.Setup(u => u.GetAll()).Returns(categoryList.Select(c => new CategoryDTO {Name = c.Name , Id = c.Id}).ToList());
            var categoryController = new CategoryController(mockUser.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), categoryController.GetAll().ToString());
        }
    }
}

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
        List<CategoryDTO> categoryList;

        [TestInitialize]
        public void Initialize()
        {
            CategoryDTO category = new CategoryDTO()
            {
                Id = 1,
                Name = "Dormir",
            };
            categoryList = new List<CategoryDTO>();
            categoryList.Add(category);
        }
        [TestMethod]
        public void GetAllCategories()
        {
            var mockCategory = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategory.Setup(u => u.GetAll()).Returns(categoryList.Select(c => new CategoryDTO {Name = c.Name , Id = c.Id}).ToList());
            var categoryController = new CategoryController(mockCategory.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), categoryController.GetAll().ToString());
        }

        [TestMethod]
        public void GetOneCategoryById()
        {
            var mockCategory = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategory.Setup(res => res.Get(categoryList[0].Id)).Returns(categoryList[0]);
            CategoryController controller = new CategoryController(mockCategory.Object);
            var result = controller.Get(categoryList[0].Id);

            mockCategory.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }
    }
}

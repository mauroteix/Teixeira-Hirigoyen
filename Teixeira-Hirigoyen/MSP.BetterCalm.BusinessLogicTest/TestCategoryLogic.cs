using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using System.Collections.Generic;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestCategoryLogic
    {
        Category category;
        Category secondCategory;
        Mock<IData<Category>> repository;
        CategoryLogic categoryLogic;
        List<Category> categoryList;

        [TestInitialize]
        public void Initialize()
        {
            category = new Category()
            {
                Id = 1,
                Name = "Dormir",
                Description = "Para tener un buen descanso",
            };
            secondCategory = new Category()
            {
                Id = 2,
                Name = "Musica",
                Description = "Cumbia pal rodri",
            };
            categoryList = new List<Category>();
            categoryList.Add(category);
            categoryList.Add(secondCategory);
            repository = new Mock<IData<Category>>();
            categoryLogic = new CategoryLogic(repository.Object);
            repository.Setup(r => r.GetAll()).Returns(categoryList);
        }
        [TestMethod]
        public void GetAll()
        {
            List<CategoryDTO> categoryList = categoryLogic.GetAll();
            Assert.AreEqual(categoryList.Count, 2);
        }
    }
}

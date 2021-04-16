using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
using System.Collections.Generic;

namespace MSP.BetterCalm.DataAccessTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        List<Category> listCategories;
        CategoryRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            listCategories = new List<Category>() {
                new Category()
                {
                    Id = 1,
                    Name = "Dormir",
                    Description = "Musica para dormir tranquilo"
                },
                new Category()
                {
                    Id=2,
                    Name="Meditar",
                    Description = "Musica para relajarse"
                }
            };
        }

        [TestMethod]
        public void GetOneCategory()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options; 
            var context = new BetterCalmContext(options);
            listCategories.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new CategoryRepository(context);
            var category = repository.Get(listCategories[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listCategories[0].Id, category.Id);
        }
    }
}

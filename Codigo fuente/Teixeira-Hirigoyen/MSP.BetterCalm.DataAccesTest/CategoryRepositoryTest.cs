using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System.Collections.Generic;
using System.Linq;

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
                    Name = "Dormir"
                },
                new Category()
                {
                    Id=2,
                    Name="Meditar"
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

        [TestMethod]
        public void AddCategory()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(listCategories[0]);
            context.SaveChanges();
            repository = new CategoryRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(listCategories[1]);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }

        [TestMethod]
        public void DeleteCategory()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            repository = new CategoryRepository(context);
            Assert.ThrowsException<CannotBePerformed>(() => repository.Delete(listCategories[0]));
        }

        [TestMethod]
        public void UpdateCategory()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options; 
            var context = new BetterCalmContext(options);
            listCategories.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new CategoryRepository(context);
            listCategories[0].Name = "Musica";
            repository.Update(listCategories[0]);
            var category = repository.Get(listCategories[0].Id);
            context.Database.EnsureDeleted();

            Assert.AreEqual(category.Name, "Musica");
        }
    }
}

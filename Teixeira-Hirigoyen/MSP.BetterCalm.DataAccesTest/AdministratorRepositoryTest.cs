using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccessTest
{
    [TestClass]
    public class AdministratorRepositoryTest
    {
        List<Administrator> listAdministrators;
        AdministratorRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            listAdministrators = new List<Administrator>() {
                new Administrator()
                {
                    Id = 1,
                    Name = "Mauro",
                    Email = "mauro@hotmail.com",
                    Password = "12345"
                },
                new Administrator()
                {
                    Id=2,
                    Name="Rodrigo",
                    Email = "rodrigo@gmail.com",
                    Password = "12345678"
                }
            };
        }

        [TestMethod]
        public void AddAdministrator()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(listAdministrators[0]);
            context.SaveChanges();
            repository = new AdministratorRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(listAdministrators[1]);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }

        [TestMethod]
        public void GetOneAdministrator()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listAdministrators.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new AdministratorRepository(context);
            var category = repository.Get(listAdministrators[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listAdministrators[0].Id, category.Id);
        }

        [TestMethod]
        public void DeleteAdministrator()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
               .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listAdministrators.ForEach(track => context.Add(track));
            context.SaveChanges();
            repository = new AdministratorRepository(context);
            repository.Delete(listAdministrators[0]);
            context.Database.EnsureDeleted();
            Administrator getAdmin = repository.Get(1);
            Assert.AreEqual(null, getAdmin);
        }

        [TestMethod]
        public void UpdateAdministrator()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listAdministrators.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new AdministratorRepository(context);
            listAdministrators[0].Name = "Nicolas";
            repository.Update(listAdministrators[0]);
            var admin = repository.Get(listAdministrators[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(admin.Name, "Nicolas");
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
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


    }
}

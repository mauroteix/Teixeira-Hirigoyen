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
    public class UserRepositoryTest
    {
        User user;
        User userSecond;
        UserRepository repository;
        [TestInitialize]
        public void Initialize()
        {
            user = new User
            {
                Name = "Mauro",
                Id = 1,
                Surname = "Teixeira",
                Birthday = new DateTime(1996, 1, 1),
                Email = "mauroGil@gmail.com",
                Cellphone = "099156189",

            };
            userSecond = new User
            {
                Name = "Rodrigo",
                Id = 2,
                Surname = "Hirigoyen",
                Birthday = new DateTime(1996, 1, 1),
                Email = "rodrigo@gmail.com",
                Cellphone = "099156189",

            };
        }
        [TestMethod]
        public void AddUser()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(user);
            context.SaveChanges();
            repository = new UserRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(userSecond);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }
    }
}

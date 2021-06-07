using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestSessionLogic
    {
        private SessionLogic sessionLogic;
        private BetterCalmContext context;
        private DbContextOptions options;
        private AdministratorRepository adminRepository;

        [TestInitialize]
        public void Initialize()
        {
            this.options = new DbContextOptionsBuilder<BetterCalmContext>().UseInMemoryDatabase(databaseName: "MSP.BetterCalmDB").Options;
            this.context = new BetterCalmContext(this.options);

            this.adminRepository = new AdministratorRepository(this.context);
            this.sessionLogic = new SessionLogic(this.adminRepository);
            Administrator admin1 = new Administrator("Mauro", "mauro@hotmail.com", "12345");
            Administrator admin2 = new Administrator("Rodrigo", "Rodrigo@hotmail.com", "4567");
            adminRepository.Add(admin1);
            adminRepository.Add(admin2);
        }

        [TestMethod]
        public void ValidateCorrectToken()
        {
            var guidToken = this.adminRepository.GetAll().ToList().FirstOrDefault().Token;
            var isCorrect = this.sessionLogic.IsCorrectToken(guidToken);
            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void ValidateIncorrectToken()
        {
            var guidToken = Guid.NewGuid();

            var isCorrect = this.sessionLogic.IsCorrectToken(guidToken);

            Assert.IsFalse(isCorrect);
        }

        [TestMethod]
        public void ValidateCorrectLogin()
        {
            var admin = new Administrator()
            {
                Email = "mauro@hotmail.com",
                Password = "12345"
            };

            var guidExpected = this.adminRepository.GetAll().ToList().FirstOrDefault(u => u.Email.Equals(admin.Email)).Token;

            var token = this.sessionLogic.Login(admin.Email, admin.Password);

            Assert.AreEqual(guidExpected, token);
        }

        [TestMethod]
        [ExpectedException(typeof(FieldEnteredNotCorrect), "The email and password cannot be empty")]
        public void ValidateNullPasswordLogin()
        {
            var adminLog = new Administrator()
            {
                Email = "mauro@hotmail.com",
                Password = ""
            };

            var token = this.sessionLogic.Login(adminLog.Email, adminLog.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(FieldEnteredNotCorrect), "The email and password cannot be empty")]
        public void ValidateNullEmailLogin()
        {
            var adminLog = new Administrator()
            {
                Email = null,
                Password = "1234"
            };

            var token = this.sessionLogic.Login(adminLog.Email, adminLog.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotExists), "The login admin is incorrect")]
        public void ValidateInCorrectLoginTest()
        {
            var adminLog = new Administrator()
            {
                Email = "mauroo@hotmail.com",
                Password = "12345"
            };

            var token = this.sessionLogic.Login(adminLog.Email, adminLog.Password);
        }
    }
}

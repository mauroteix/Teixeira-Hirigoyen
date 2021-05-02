﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
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
    }
}

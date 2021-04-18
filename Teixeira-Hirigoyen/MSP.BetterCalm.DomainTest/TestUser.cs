﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestUser
    {
        User user;
        [TestInitialize]
        public void Initialize()
        {
            user = new User
            {
                Name = "Mauro",
                Id = 1,
                Surname = "Teixeira",
                Birthday = new DateTime(1996,1,1),
                Email = "mauroGil@gmail.com",
                Cellphone = "099156189",

            };
            

        }
        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Mauro", user.Name);
        }
        [TestMethod]
        public void RegisterSurname()
        {
            Assert.AreEqual("Teixeira", user.Surname);
        }
        [TestMethod]
        public void RegisterBirthday()
        {
            DateTime date = new DateTime(1996, 1, 1);
            Assert.AreEqual(date, user.Birthday);
        }
        [TestMethod]
        public void RegisterEmail()
        {
            Assert.AreEqual("mauroGil@gmail.com", user.Email);
        }
        [TestMethod]
        public void RegisterCellphone()
        {
            Assert.AreEqual("099156189", user.Cellphone);
        }
    }
}

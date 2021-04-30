using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestAdministrator
    {
        Administrator administrator;
        [TestInitialize]
        public void Initialize()
        {
            administrator = new Administrator
            {
                Name = "Rodrigo",
                Id = 1,
                Email = "mauroGil@gmail.com",
                Password = "123"
            };
        }
        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Rodrigo", administrator.Name);
        }
        [TestMethod]
        public void RegisterEmail()
        {
            Assert.AreEqual("mauroGil@gmail.com", administrator.Email);
        }
        [TestMethod]
        public void RegisterPassword()
        {
            Assert.AreEqual("123", administrator.Password);
        }

        [TestMethod]
        public void NameEmpty()
        {
            administrator.Name = "";
            Assert.IsTrue(administrator.NameEmpty());
        }

        [TestMethod]
        public void EmailEmpty()
        {
            administrator.Email = "";
            Assert.IsTrue(administrator.EmailEmpty());
        }

        [TestMethod]
        public void PasswordEmpty()
        {
            administrator.Password = "";
            Assert.IsTrue(administrator.PasswordEmpty());
        }
    }
}

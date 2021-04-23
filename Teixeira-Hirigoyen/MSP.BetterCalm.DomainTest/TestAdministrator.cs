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

    }
}

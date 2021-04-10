using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestCategory
    {

        Category category;
        [TestInitialize]
        public void Initialize()
        {
            category = new Category();
        }

        [TestMethod]
        public void RegisterName()
        {
            category.Name = "Mauro";
            Assert.AreEqual("Mauro", category.Name);
        }

        [TestMethod]
        public void RegisterDescription()
        {
            category.Description = "Facil para dormir";
            Assert.AreEqual("Facil para dormir", category.Description);
        }
    }
}

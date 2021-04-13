using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestCategoryTrack
    {
        CategoryTrack categoryTrack;
        Category category;

        [TestInitialize]
        public void Initialize()
        {
            categoryTrack = new CategoryTrack();
            category = new Category
            {   Name = "Dormir",
                Id = 0,
                Description = "Para relajarte"
            };
            
        }

        [TestMethod]
        public void RegisterCategory()
        {
            categoryTrack.Category = category;
            Assert.AreEqual(category, categoryTrack.Category);
        }

    }
}

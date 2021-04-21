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
        Track track;

        [TestInitialize]
        public void Initialize()
        {
            categoryTrack = new CategoryTrack();
            category = new Category
            {   Name = "Dormir",
                Id = 0
            };
            track = new Track
            {
                Name = "Llegamos",
                Id = 1,
                Author = "Los pibes chorros"
            };
            
        }

        [TestMethod]
        public void RegisterCategory()
        {
            categoryTrack.Category = category;
            Assert.AreEqual(category, categoryTrack.Category);
        }

        [TestMethod]
        public void RegisterCategoryId()
        {
            categoryTrack.IdCategory = category.Id;
            Assert.AreEqual(category.Id, categoryTrack.IdCategory);
        }

        [TestMethod]
        public void RegisterTrack()
        {
            categoryTrack.Track= track;
            Assert.AreEqual(track, categoryTrack.Track);
        }

        [TestMethod]
        public void RegisterTrackId()
        {
            categoryTrack.IdTrack = track.Id;
            Assert.AreEqual(track.Id, categoryTrack.IdTrack);
        }

    }
}

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
        Track track;
        Playlist playlist;
        CategoryTrack categoryTrack;

        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist();
            playlist.Id = 1;
            playlist.Name = "Musica para Mauro";
            category = new Category
            {
                Name = "Dormir",
                Id = 1,
                Description = "Facil para dormir",
                CategoryTrack = new List<CategoryTrack>(),
               
            };
            track = new Track
            {
                Id = 1,
                Image = "",
                Author = "",
                Name = "Mauro baila cumbia"
                
            };
            categoryTrack = new CategoryTrack
            {
                IdCategory = category.Id,
                Category = category,
                IdTrack = track.Id,
                Track = track,
            };
            category.CategoryTrack.Add(categoryTrack);

        }

        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Dormir", category.Name);
        }

        [TestMethod]
        public void RegisterDescription()
        {
            Assert.AreEqual("Facil para dormir", category.Description);
        }

        [TestMethod]
        public void RegisterCategoryTrack()
        {
            Assert.IsTrue(category.CategoryTrack.Count == 1);
        }
        
    }
}

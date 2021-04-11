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
        [TestInitialize]
        public void Initialize()
        {
            category = new Category();
            playlist = new Playlist();
            track = new Track();
            
            track.Id = 1;
            track.Image = "";
            track.Author = "";
            track.Name = "Mauro baila cumbia";

            playlist.Id = 1;
            playlist.Name = "Musica para Mauro";

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
        [TestMethod]
        public void RegisterTrack()
        {
            category.Track = track;
            Assert.IsTrue(category.Track.Id == track.Id);
        }
        [TestMethod]
        public void RegisterPlayList()
        {
            category.Playlist = playlist;
            Assert.IsTrue(category.Playlist.Id == playlist.Id);
        }
    }
}

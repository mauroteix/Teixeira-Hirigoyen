using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPlaylist
    {
        Playlist playlist;
        Track track;
        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist();
            track = new Track();
            track.Id = 1;
            track.Image = "";
            track.Author = "";
            track.Name = "Mauro baila cumbia";

        }

        [TestMethod]
        public void NameEmpty()
        {
            playlist.Name = "";
            Assert.IsTrue(playlist.NameEmpty());
        }
        [TestMethod]
        public void RegisterName()
        {
            playlist.Name = "Mauro";
            Assert.AreEqual("Mauro", playlist.Name);
        }
        [TestMethod]
        public void RegisterDescription()
        {
            playlist.Description = "Facil para dormir";
            Assert.AreEqual("Facil para dormir", playlist.Description);
        }
        [TestMethod]
        public void DescriptionLengthTrue()
        {
            playlist.Description = "Facil para dormir";
            Assert.IsTrue(playlist.DescriptionLength());
        }
        [TestMethod]
        public void DescriptionLengthFalse()
        {
            playlist.Description = "Facil para dormir asd qweqwe 1231231 seasdas gffdgdf asdqwe qqwe eqwe qweqw eqwe "+
                "qw eqw eqweqw eqwe qw eqw eq qweqw eqwe qwe qw eq qweq weqwe "+
                "qwe qwe qwe qwe qwe qwe qweqw eq weqw eqw eqwe qw eqeqweqwe q weqwe qweqweqw eqw";
            Assert.IsFalse(playlist.DescriptionLength());
        }
        [TestMethod]
        public void RegisterImage()
        {
            playlist.Image = "http://www.google.com";
            Assert.AreEqual("http://www.google.com", playlist.Image);
        }
        [TestMethod]
        public void ImageEmpty()
        {
            playlist.Image = "";
            Assert.IsTrue(playlist.ImageEmpty());
        }
        [TestMethod]
        public void RegisterTrack()
        {
            playlist.Track = track;
            Assert.IsTrue(playlist.Track.Id == track.Id);
        }
    }
}

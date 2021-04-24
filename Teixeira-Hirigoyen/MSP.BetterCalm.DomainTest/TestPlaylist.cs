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
        Category category;
        PlaylistTrack playlistTrack;
        PlaylistCategory playlistCategory;

        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist {
                Id = 2,
                Name = "Mauro",
                Description = "Facil para dormir",
                Image = "",
                PlaylistTrack = new List<PlaylistTrack>(),
                PlaylistCategory = new List<PlaylistCategory>()
        };
            track = new Track {
                Id = 1,
                Image = "",
                Author = "",
                Sound = "",
                Name = "Mauro baila cumbia",
                CategoryTrack = new List<CategoryTrack>(),
            };
            category = new Category
            {
                Id = 1,
                Name = "Dormir"
            };

            playlistTrack = new PlaylistTrack
            {
                IdPlaylist = playlist.Id,
                Playlist = playlist,
                IdTrack = track.Id,
                Track = track,
            };

            playlistCategory = new PlaylistCategory
            {
                IdPlaylist = playlist.Id,
                Playlist = playlist,
                IdCategory = category.Id,
                Category = category
            };
            playlist.PlaylistTrack.Add(playlistTrack);
            playlist.PlaylistCategory.Add(playlistCategory);

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
            Assert.AreEqual("Mauro", playlist.Name);
        }
        [TestMethod]
        public void RegisterDescription()
        {
            Assert.AreEqual("Facil para dormir", playlist.Description);
        }
        [TestMethod]
        public void DescriptionLengthTrue()
        {
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
        public void RegisterPlaylistTrack()
        {
            Assert.IsTrue(playlist.PlaylistTrack.Count == 1);
        }

        [TestMethod]
        public void RegisterPlaylistCategory()
        {
            Assert.IsTrue(playlist.PlaylistCategory.Count == 1);
        }

        [TestMethod]
        public void PlaylistCategoryNotEmpty()
        {
            Assert.IsFalse(playlist.PlaylistCategoryEmpty());
        }

    }
}

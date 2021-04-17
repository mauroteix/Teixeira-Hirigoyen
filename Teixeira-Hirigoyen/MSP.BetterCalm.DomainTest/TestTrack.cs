using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System.Collections.Generic;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestTrack
    {
        Track track;
        Category category;
        Playlist playlist;
        CategoryTrack categoryTrack;
        PlaylistTrack playlistTrack;

        [TestInitialize]
        public void Initialize()
        {
            track = new Track
            {
                Id = 1,
                Image = "",
                Author = "",
                Sound = "",
                Name = "Mauro baila cumbia",
                Hour = 1,
                MinSeconds = 2.10,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
           
            category = new Category {
                Id = 1,
                Name = "Musica",
                Description = "Facil para dormir",
                CategoryTrack = new List<CategoryTrack>(),
            };
            categoryTrack = new CategoryTrack
            {
                IdCategory = category.Id,
                Category = category,
                IdTrack = track.Id,
                Track = track,
            };
            track.CategoryTrack.Add(categoryTrack);

            playlist = new Playlist {
                Id = 1,
                Name = "Musica para Mauro",
                Description = "Variado pop, rock, etc",
                Image ="",
                PlaylistTrack = new List<PlaylistTrack>() 
            };

            playlistTrack = new PlaylistTrack
            {
                IdPlaylist = playlist.Id,
                Playlist = playlist,
                IdTrack = track.Id,
                Track = track
            };
            track.PlaylistTrack.Add(playlistTrack);
        }

        [TestMethod]
        public void NameEmpty()
        {
            track.Name = "";
            Assert.IsTrue(track.NameEmpty());
        }

        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Mauro baila cumbia", track.Name);
        }

        [TestMethod]
        public void AuthorEmpty()
        {
            Assert.IsTrue(track.AuthorEmpty());
        }

        [TestMethod]
        public void RegisterAuthor()
        {
            track.Author = "NTVG";
            Assert.AreEqual("NTVG", track.Author);
        }

        [TestMethod]
        public void ImageEmpty()
        {
            Assert.IsTrue(track.ImageEmpty());
        }

        [TestMethod]
        public void RegisterImage()
        {
            track.Image = "http://www.google.com";
            Assert.AreEqual("http://www.google.com", track.Image);
        }

        [TestMethod]
        public void RegisterSound()
        {
            track.Sound = "http://www.youtube.com";
            Assert.AreEqual("http://www.youtube.com", track.Sound);
        }

        [TestMethod]
        public void SoundEmpty()
        {
            Assert.IsTrue(track.SoundEmpty());
        }

        [TestMethod]
        public void RegisterCategoryTrack()
        {
            Assert.IsTrue(track.CategoryTrack.Count == 1);
        }

        [TestMethod]
        public void RegisterPlaylistTrack()
        {
            Assert.IsTrue(track.PlaylistTrack.Count == 1);
        }

        [TestMethod]
        public void RegisterHour()
        {
            Assert.AreEqual(1, track.Hour);
        }

    }
}

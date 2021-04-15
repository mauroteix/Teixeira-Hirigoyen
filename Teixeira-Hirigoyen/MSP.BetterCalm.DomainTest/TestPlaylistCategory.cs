using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPlaylistCategory
    {
        Playlist playlist;
        Category category;
        PlaylistCategory playlistCategory;

        [TestInitialize]
        public void Initialize()
        {
            playlistCategory = new PlaylistCategory();
            playlist = new Playlist
            {
                Name = "Cumbia",
                Id = 1,
                Description = "Boliche",
                Image = "",
                PlaylistTrack = new List<PlaylistTrack>()
            };
            category = new Category
            {
                Name = "Dormir",
                Id = 2,
                Description = "Musica para relajarse",
                CategoryTrack = new List<CategoryTrack>()
            };
        }

        [TestMethod]
        public void RegisterPlaylist()
        {
            playlistCategory.Playlist = playlist;
            Assert.AreEqual(playlist, playlistCategory.Playlist);
        }
        [TestMethod]
        public void RegisterPlaylistId()
        {
            playlistCategory.IdPlaylist = playlist.Id;
            Assert.AreEqual(playlist.Id, playlistCategory.IdPlaylist);
        }

        [TestMethod]
        public void RegisterCategory()
        {
            playlistCategory.Category = category;
            Assert.AreEqual(category, playlistCategory.Category);
        }
    }
}

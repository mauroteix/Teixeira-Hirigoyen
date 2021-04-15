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
    }
}

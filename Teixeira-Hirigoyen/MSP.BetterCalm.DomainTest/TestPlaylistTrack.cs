using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPlaylistTrack
    {
        Playlist playlist;
        PlaylistTrack playlistTrack;

        [TestInitialize]
        public void Initialize()
        {
            playlistTrack = new PlaylistTrack();
            playlist = new Playlist
            {
                Name = "Cumbia",
                Id = 1,
                Description = "Boliche",
                Image = "",
            };
         
        }


        [TestMethod]
        public void RegisterPlaylist()
        {
            playlistTrack.Playlist = playlist;
            Assert.AreEqual(playlist, playlistTrack.Playlist);
        }

        [TestMethod]
        public void RegisterPlaylistId()
        {
            playlistTrack.IdPlaylist = playlist.Id;
            Assert.AreEqual(playlist.Id, playlistTrack.IdPlaylist);
        }
    }
}

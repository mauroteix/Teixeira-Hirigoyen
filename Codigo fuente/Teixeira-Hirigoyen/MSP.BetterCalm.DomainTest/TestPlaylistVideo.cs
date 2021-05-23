using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPlaylistVideo
    {
        Playlist playlist;
        PlaylistVideo playlistVideo;

        [TestInitialize]
        public void Initialize()
        {
            playlistVideo = new PlaylistVideo();
            playlist = new Playlist
            {
                Name = "Cumbia",
                Id = 1,
                Description = "Boliche",
                Image = ""
            };
        }

        [TestMethod]
        public void RegisterPlaylistId()
        {
            playlistVideo.IdPlaylist = playlist.Id;
            Assert.AreEqual(playlist.Id, playlistVideo.IdPlaylist);
        }

    }
}

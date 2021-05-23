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
        Video video;
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
            video = new Video
            {
                Id = 1,
                Name = "Bailando",
                Author = "Mauro",
                Hour = 1,
                MinSeconds = 2.10,
                LinkVideo = "www.youtube.com/videomauro",
                CategoryVideo = new List<CategoryVideo>()
            };
        }

        [TestMethod]
        public void RegisterPlaylistId()
        {
            playlistVideo.IdPlaylist = playlist.Id;
            Assert.AreEqual(playlist.Id, playlistVideo.IdPlaylist);
        }

        [TestMethod]
        public void RegisterPlaylist()
        {
            playlistVideo.Playlist = playlist;
            Assert.AreEqual(playlist, playlistVideo.Playlist);
        }

        [TestMethod]
        public void RegisterVideoId()
        {
            playlistVideo.IdVideo = video.Id;
            Assert.AreEqual(video.Id, playlistVideo.IdVideo);
        }
    }
}

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
        Track track;
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
                Image = ""
            };
            track = new Track
            {
                Id = 1,
                Name = "Otra vez",
                Author = "Wisin",
                Image = "",
                Sound = "",
                CategoryTrack = new List<CategoryTrack>()
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

        [TestMethod]
        public void RegisterTrack()
        {
            playlistTrack.Track = track;
            Assert.AreEqual(track, playlistTrack.Track);
        }

        [TestMethod]
        public void RegisterTrackId()
        {
            playlistTrack.IdTrack = track.Id;
            Assert.AreEqual(track.Id, playlistTrack.IdTrack);
        }
    }
}

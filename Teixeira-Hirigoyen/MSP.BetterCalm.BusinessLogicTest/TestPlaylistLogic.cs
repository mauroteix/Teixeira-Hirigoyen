using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestPlaylistLogic
    {
        Playlist playlist;
        List<Playlist> playlistList = new List<Playlist>();
        Mock<IData<Playlist>> repositoryPlaylist;
        PlaylistLogic playlistLogic;

        [TestInitialize]
        public void Initialize()
        {
            playlist = new Playlist()
            {
                Id = 0,
                Name = "Reggaeton",
                Description = "Old hits, daddy yankee",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            playlistList.Add(playlist);
            repositoryPlaylist = new Mock<IData<Playlist>>();
            repositoryPlaylist.Setup(r => r.GetAll()).Returns(playlistList);

            repositoryPlaylist.Setup(play => play.Get(0)).Returns(playlist);
            repositoryPlaylist.Setup(play => play.Add(playlist));
            playlistLogic = new PlaylistLogic(repositoryPlaylist.Object);
        }

        [TestMethod]
        public void GetPlaylist()
        {
            Playlist newPlaylist = playlistLogic.Get(playlist.Id);
            Assert.AreEqual(playlist, newPlaylist);
        }

    }
    }

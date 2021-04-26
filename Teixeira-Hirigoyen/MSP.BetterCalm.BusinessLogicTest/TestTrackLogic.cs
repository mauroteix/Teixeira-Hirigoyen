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
    public class TestTrackLogic
    {

        Track track;
        List<Track> trackList = new List<Track>();
        Mock<IData<Track>> repositoryTrack;
       // Mock<IData<Category>> repositoryCategory;
        TrackLogic trackLogic;
        //List<Category> categoryList = new List<Category>();

        [TestInitialize]
        public void Initialize()
        {
            track = new Track()
            {
                Id = 0,
                Name = "Reggaeton",
                Description = "Old hits, daddy yankee",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            playlistList.Add(playlist);
            repositoryPlaylist = new Mock<IData<Playlist>>();
            repositoryCategory = new Mock<IData<Category>>();

            repositoryPlaylist.Setup(r => r.GetAll()).Returns(playlistList);
            repositoryCategory.Setup(r => r.GetAll()).Returns(categoryList);

            repositoryPlaylist.Setup(play => play.Get(0)).Returns(playlist);
            repositoryPlaylist.Setup(play => play.Add(playlist));
            playlistLogic = new PlaylistLogic(repositoryPlaylist.Object, repositoryCategory.Object);

        }

        [TestMethod]
        public void GetPlaylist()
        {
            Playlist newPlaylist = playlistLogic.Get(playlist.Id);
            Assert.AreEqual(playlist, newPlaylist);
        }

    }
}

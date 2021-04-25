using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.APITest
{
    [TestClass]
    public class PlaylistControllerTest
    {
        List<Playlist> playlistList;

        [TestInitialize]
        public void Initialize()
        {
            Playlist playlist = new Playlist()
            {
                Id = 1,
                Name = "Variado 2021",
                Description = "Nuevos hits",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            playlistList = new List<Playlist>();
            playlistList.Add(playlist);
        }

        [TestMethod]
        public void GetOnePlaylist()
        {
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(res => res.Get(playlistList[0].Id)).Returns(playlistList[0]);
            PlaylistController controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.Get(playlistList[0].Id);

            mockPlaylist.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }


        [TestMethod]
        public void GetOnePlaylistError()
        {
           
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(res => res.Get(playlistList[0].Id)).Throws(new Exception());
            PlaylistController controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.Get(playlistList[0].Id);

            mockPlaylist.VerifyAll();
            Assert.AreEqual(result.ToString(), new ObjectResult("").ToString());
        }
    }
}

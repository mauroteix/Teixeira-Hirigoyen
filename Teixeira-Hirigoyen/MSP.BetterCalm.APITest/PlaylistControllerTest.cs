using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
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

        [TestMethod]
        public void AddOnePlaylist()
        {
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(res => res.Get(playlistList[0].Id)).Returns(playlistList[0]);
            PlaylistController controller = new PlaylistController(mockPlaylist.Object);

            var result = controller.Add(playlistList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(playlistList[0]).ToString());
        }

        [TestMethod]
        public void AddPlaylistError()
        {
            playlistList[0].Name = "";
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(r => r.Add(playlistList[0])).Throws(new FieldEnteredNotCorrect(""));
            PlaylistController controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.Add(playlistList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void DeletePlaylistOk()
        {
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(t => t.Get(1)).Returns(playlistList[0]);
            mockPlaylist.Setup(t => t.Delete(playlistList[0]));
            var controller = new PlaylistController(mockPlaylist.Object);
            controller.Add(playlistList[0]);
            var result = controller.DeletePlaylist(1);
            Assert.AreEqual(new OkObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeletePlaylistIdNegative()
        {
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            var controller = new PlaylistController(mockPlaylist.Object);
            controller.Add(playlistList[0]);
            var result = controller.DeletePlaylist(-2);
            Assert.AreEqual(new NotFoundObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeletePlaylistNotExists()
        {
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(l => l.Get(1)).Returns(playlistList[0]);
            var controller = new PlaylistController(mockPlaylist.Object);

            var result = controller.DeletePlaylist(3);
            Assert.AreEqual(new ObjectResult("").ToString(),
                result.ToString());
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using UruguayNatural.HandleError;

namespace MSP.BetterCalm.APITest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PlaylistControllerTest
    {
        List<Playlist> playlistList;

        [TestInitialize]
        public void Initialize()
        {
            Category category = new Category()
            {
                Id = 1,
                Name = "Musica",
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistCategory = new List<PlaylistCategory>()
            };
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
        public void AddPlaylistErrorCategoryUnique()
        {

            List<PlaylistCategory> list = new List<PlaylistCategory>();
            PlaylistCategory play = new PlaylistCategory
            {
                IdCategory = 1
            };
            PlaylistCategory play2 = new PlaylistCategory
            {
                IdCategory = 1
            };
            list.Add(play);
            list.Add(play2);
            playlistList[0].PlaylistCategory = list;
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(r => r.Add(playlistList[0])).Throws(new EntityAlreadyExist(""));
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

        [TestMethod]
        public void UpdatePlaylistErrorCategoryEmpty()
        {
            Playlist newPlaylist = new Playlist()
            {
                Id = 1,
                Name = "Cumbia",
                Description = "Old hits",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(l => l.Get(playlistList[0].Id)).Returns(playlistList[0]);
            mockPlaylist.Setup(l => l.Update(playlistList[0], 1)).Throws(new FieldEnteredNotCorrect("A Playlist Category must be added"));
            var controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.UpdatePlaylist(1, newPlaylist);
            Mock.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdatePlaylistErrorNameEmpty()
        {
            Playlist newPlaylist = new Playlist()
            {
                Id = 1,
                Name = "",
                Description = "Old hits",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(l => l.Get(playlistList[0].Id)).Returns(playlistList[0]);
            mockPlaylist.Setup(l => l.Update(playlistList[0], 1)).Throws(new FieldEnteredNotCorrect("The name cannot be empty"));
            var controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.UpdatePlaylist(1, newPlaylist);
            Mock.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdatePlaylistEntityNotExist()
        {
            Playlist newPlaylist = new Playlist()
            {
                Id = 1,
                Name = "",
                Description = "Old hits",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            PlaylistCategory play = new PlaylistCategory
            {
                IdCategory = 100,
            };
            playlistList[0].PlaylistCategory.Add(play);
            newPlaylist.PlaylistCategory.Add(play);
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(l => l.Get(playlistList[0].Id)).Returns(playlistList[0]);
            mockPlaylist.Setup(l => l.Update(playlistList[0], 1)).Throws(new EntityNotExists("One ore more category do not exist"));
            var controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.UpdatePlaylist(1, newPlaylist);
            Mock.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdatePlaylistEntityAlreadyExist()
        {
            Playlist newPlaylist = new Playlist()
            {
                Id = 1,
                Name = "",
                Description = "Old hits",
                PlaylistCategory = new List<PlaylistCategory>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            PlaylistCategory play = new PlaylistCategory
            {
                IdCategory = 1,
            };
            PlaylistCategory play2 = new PlaylistCategory
            {
                IdCategory = 1,
            };
            playlistList[0].PlaylistCategory.Add(play);
            newPlaylist.PlaylistCategory.Add(play);
            newPlaylist.PlaylistCategory.Add(play2);
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(l => l.Get(playlistList[0].Id)).Returns(playlistList[0]);
            mockPlaylist.Setup(l => l.Update(playlistList[0], 1)).Throws(new EntityAlreadyExist("There are two or more equal categories"));
            var controller = new PlaylistController(mockPlaylist.Object);
            var result = controller.UpdatePlaylist(1, newPlaylist);
            Mock.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void GetAllPlaylists()
        {
            var mockPlaylist = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(u => u.GetAll()).Returns(playlistList);
            var playlistController = new PlaylistController(mockPlaylist.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), playlistController.GetAll().ToString());
        }
    }
}

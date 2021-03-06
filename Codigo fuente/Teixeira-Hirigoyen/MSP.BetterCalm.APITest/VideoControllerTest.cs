using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MSP.BetterCalm.APITest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class VideoControllerTest
    {
        List<Video> videoList;

        [TestInitialize]
        public void Initialize()
        {
            Category category = new Category
            {
                Name = "Musica",
                Id = 2
            };
            Video video = new Video()
            {
                Id = 1,
                Name = "Despacito",
                Author = "Luis Fonsi",
                LinkVideo = "despacito-fonsi.com",
                Hour = 0,
                MinSeconds = 3.2,
                CategoryVideo = new List<CategoryVideo>(),
                PlaylistVideo = new List<PlaylistVideo>()
            };
            CategoryVideo categoryVideo = new CategoryVideo
            {
                IdCategory = category.Id
            };
            video.CategoryVideo.Add(categoryVideo);
            videoList = new List<Video>();
            videoList.Add(video);
        }

        [TestMethod]
        public void GetOneVideo()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(res => res.Get(videoList[0].Id)).Returns(videoList[0]);
            VideoController controller = new VideoController(mockVideo.Object);
            var result = controller.Get(videoList[0].Id);

            mockVideo.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }

        [TestMethod]
        public void GetOneVideoError()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(res => res.Get(videoList[0].Id)).Throws(new Exception());
            VideoController controller = new VideoController(mockVideo.Object);
            var result = controller.Get(videoList[0].Id);

            mockVideo.VerifyAll();
            Assert.AreEqual(result.ToString(), new ObjectResult("").ToString());
        }

        [TestMethod]
        public void AddOneVideo()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(res => res.Get(videoList[0].Id)).Returns(videoList[0]);
            VideoController controller = new VideoController(mockVideo.Object);

            var result = controller.Add(videoList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(videoList[0]).ToString());
        }

        [TestMethod]
        public void AddVideoError()
        {
            videoList[0].Name = "";
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(r => r.Add(videoList[0])).Throws(new FieldEnteredNotCorrect(""));
            VideoController controller = new VideoController(mockVideo.Object);
            var result = controller.Add(videoList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void UpdateVideo()
        {
            Video newVideo = new Video()
            {
                Name = "Puntos",
                Author = "Rodri"
            };
            var mockVideo= new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(l => l.Get(videoList[0].Id)).Returns(videoList[0]);
            mockVideo.Setup(l => l.Add(videoList[0]));
            var controller = new VideoController(mockVideo.Object);
            videoList[0].Name = newVideo.Name;
            videoList[0].Author = newVideo.Author;
            var result = controller.Update(videoList[0].Id, videoList[0]);
            Assert.AreEqual(new ObjectResult("Updated successfully").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdateVideoError()
        {
            Video newVideo = new Video()
            {
                Name = "Puntos",
                Author = "Rodri"
            };
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(l => l.Get(videoList[0].Id)).Returns(videoList[0]);
            mockVideo.Setup(l => l.Update(videoList[0], 1)).Throws(new FieldEnteredNotCorrect(""));
            var controller = new VideoController(mockVideo.Object);
            videoList[0].Name = newVideo.Name;
            videoList[0].Author = newVideo.Author;
            var result = controller.Update(videoList[0].Id, videoList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteVideoOk()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(t => t.Get(1)).Returns(videoList[0]);
            mockVideo.Setup(t => t.Delete(videoList[0]));
            var controller = new VideoController(mockVideo.Object);
            controller.Add(videoList[0]);
            var result = controller.Delete(1);
            Assert.AreEqual(new OkObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteVideoIdNegative()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            var controller = new VideoController(mockVideo.Object);
            controller.Add(videoList[0]);
            var result = controller.Delete(-2);
            Assert.AreEqual(new NotFoundObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteVideoNotExists()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(l => l.Get(1)).Returns(videoList[0]);
            var controller = new VideoController(mockVideo.Object);

            var result = controller.Delete(3);
            Assert.AreEqual(new ObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void GetAllVideo()
        {
            var mockVideo = new Mock<IVideoLogic>(MockBehavior.Strict);
            mockVideo.Setup(u => u.GetAll()).Returns(videoList);
            var trackController = new VideoController(mockVideo.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), trackController.GetAll().ToString());
        }
    }
}

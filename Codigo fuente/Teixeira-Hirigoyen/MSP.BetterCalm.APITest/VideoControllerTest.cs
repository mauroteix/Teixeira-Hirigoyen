using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
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


    }
}

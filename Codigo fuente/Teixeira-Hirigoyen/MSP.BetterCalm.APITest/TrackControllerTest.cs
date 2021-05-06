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
    public class TrackControllerTest
    {
        List<Track> trackList;

        [TestInitialize]
        public void Initialize()
        {
            Category category = new Category
            {
                Name = "Musica",
                Id = 2
            };
            Track track = new Track()
            {
                Id = 1,
                Name = "Despacito",
                Author = "Luis Fonsi",
                Sound = "despacito-fonsi.mp3",
                Hour = 0,
                MinSeconds = 3.2,
                CategoryTrack = new List<CategoryTrack>(),
                PlaylistTrack = new List<PlaylistTrack>()
            };
            CategoryTrack categoryTrack = new CategoryTrack
            {
                IdCategory = category.Id
            };
            track.CategoryTrack.Add(categoryTrack);
            trackList = new List<Track>();
            trackList.Add(track);
        }

        [TestMethod]
        public void GetOneTrack()
        {
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(res => res.Get(trackList[0].Id)).Returns(trackList[0]);
            TrackController controller = new TrackController(mockTrack.Object);
            var result = controller.Get(trackList[0].Id);

            mockTrack.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }

        [TestMethod]
        public void GetOneTrackError()
        {
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(res => res.Get(trackList[0].Id)).Throws(new Exception());
            TrackController controller = new TrackController(mockTrack.Object);
            var result = controller.Get(trackList[0].Id);

            mockTrack.VerifyAll();
            Assert.AreEqual(result.ToString(), new ObjectResult("").ToString());
        }

        [TestMethod]
        public void AddOneTrack()
        {
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(res => res.Get(trackList[0].Id)).Returns(trackList[0]);
            TrackController controller = new TrackController(mockTrack.Object);

            var result = controller.Add(trackList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(trackList[0]).ToString());
        }

        [TestMethod]
        public void AddTrackError()
        {
            trackList[0].Name = "";
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(r => r.Add(trackList[0])).Throws(new FieldEnteredNotCorrect(""));
            TrackController controller = new TrackController(mockTrack.Object);
            var result = controller.Add(trackList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void UpdateTrack()
        {
            Track newTrack = new Track()
            {
                Name = "Puntos",
                Author = "Rodri"
            };
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(l => l.Get(trackList[0].Id)).Returns(trackList[0]);
            mockTrack.Setup(l => l.Add(trackList[0]));
            var controller = new TrackController(mockTrack.Object);
            trackList[0].Name = newTrack.Name;
            trackList[0].Author = newTrack.Author;
            var result = controller.Update(trackList[0].Id, trackList[0]);
            Assert.AreEqual(new ObjectResult("Updated successfully").ToString(),
                result.ToString());
        }


        [TestMethod]
        public void UpdateTrackError()
        {
            Track newTrack = new Track()
            {
                Name = "Puntos",
                Author = "Rodri"
            };
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(l => l.Get(trackList[0].Id)).Returns(trackList[0]);
            mockTrack.Setup(l => l.Update(trackList[0],1)).Throws(new FieldEnteredNotCorrect(""));
            var controller = new TrackController(mockTrack.Object);
            trackList[0].Name = newTrack.Name;
            trackList[0].Author = newTrack.Author;
            var result = controller.Update(trackList[0].Id, trackList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteTrackOk()
        {
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(t => t.Get(1)).Returns(trackList[0]);
            mockTrack.Setup(t => t.Delete(trackList[0]));
            var controller = new TrackController(mockTrack.Object);
            controller.Add(trackList[0]);
            var result = controller.DeleteTrack(1);
            Assert.AreEqual(new OkObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteTrackIdNegative()
        {
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            var controller = new TrackController(mockTrack.Object);
            controller.Add(trackList[0]);
            var result = controller.DeleteTrack(-2);
            Assert.AreEqual(new NotFoundObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteTrackNotExists()
        {
            var mockTrack = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTrack.Setup(l => l.Get(1)).Returns(trackList[0]);
            var controller = new TrackController(mockTrack.Object);

            var result = controller.DeleteTrack(3);
            Assert.AreEqual(new ObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void GetAllTracks()
        {
            var mockTracks = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockTracks.Setup(u => u.GetAll()).Returns(trackList);
            var trackController = new TrackController(mockTracks.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), trackController.GetAll().ToString());
        }
    }
}

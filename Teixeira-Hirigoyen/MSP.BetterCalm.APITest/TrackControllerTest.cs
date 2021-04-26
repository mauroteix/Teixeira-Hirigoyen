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
    public class TrackControllerTest
    {
        List<Track> trackList;

        [TestInitialize]
        public void Initialize()
        {
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
            trackList = new List<Track>();
            trackList.Add(track);
        }

        [TestMethod]
        public void GetOneTrack()
        {
            var mockPlaylist = new Mock<ITrackLogic>(MockBehavior.Strict);
            mockPlaylist.Setup(res => res.Get(trackList[0].Id)).Returns(trackList[0]);
            TrackController controller = new TrackController(mockPlaylist.Object);
            var result = controller.Get(trackList[0].Id);

            mockPlaylist.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }
    }
}

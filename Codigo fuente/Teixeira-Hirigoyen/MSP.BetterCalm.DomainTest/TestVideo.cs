using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestVideo
    {
        Video video;

        [TestInitialize]
        public void Initialize()
        {
            video = new Video
            {
                Id = 1,
                Name = "Bailando",
                Author = "Mauro",
                Hour = 1,
                MinSeconds = 2.10
            };

        }

        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Bailando", video.Name);
        }

        [TestMethod]
        public void NameEmpty()
        {
            video.Name = "";
            Assert.IsTrue(video.NameEmpty());
        }

        [TestMethod]
        public void RegisterAuthor()
        {
            Assert.AreEqual("Mauro", video.Author);
        }

        [TestMethod]
        public void AuthorEmpty()
        {
            video.Author = "";
            Assert.IsTrue(video.AuthorEmpty());
        }

        [TestMethod]
        public void RegisterHour()
        {
            Assert.AreEqual(1, video.Hour);
        }

        [TestMethod]
        public void HourIsEmpty()
        {
            video.Hour = 0;
            Assert.IsTrue(video.HourIsEmpty());
        }

        [TestMethod]
        public void RegisterMinSeconds()
        {
            Assert.AreEqual(2.10, video.MinSeconds);
        }

        [TestMethod]
        public void MinSecondsIsEmpty()
        {
            video.MinSeconds = 0;
            Assert.IsTrue(video.MinSecondsIsEmpty());
        }

    }
}

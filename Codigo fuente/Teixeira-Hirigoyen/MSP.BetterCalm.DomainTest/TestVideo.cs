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
                Name = "Bailando"
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
    }
}

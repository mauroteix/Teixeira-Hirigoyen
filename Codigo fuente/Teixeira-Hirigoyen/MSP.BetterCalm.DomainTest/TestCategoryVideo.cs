using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestCategoryVideo
    {
        CategoryVideo categoryVideo;
        Category category;
        Video video;

        [TestInitialize]
        public void Initialize()
        {
            categoryVideo = new CategoryVideo();
            category = new Category
            {
                Name = "Dormir",
                Id = 0
            };
            video = new Video
            {
                Id = 1,
                Name = "Bailando",
                Author = "Mauro",
                MinSeconds = 2.10,
                LinkVideo = "www.youtube.com/bailando"
            };

        }

        [TestMethod]
        public void RegisterCategory()
        {
            categoryVideo.Category = category;
            Assert.AreEqual(category, categoryVideo.Category);
        }

        [TestMethod]
        public void RegisterCategoryId()
        {
            categoryVideo.IdCategory = category.Id;
            Assert.AreEqual(category.Id, categoryVideo.IdCategory);
        }

        [TestMethod]
        public void RegisterVideo()
        {
            categoryVideo.Video = video;
            Assert.AreEqual(video, categoryVideo.Video);
        }
    }
}

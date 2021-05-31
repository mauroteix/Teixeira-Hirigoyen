using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestVideoLogic
    {
        Video video;
        List<Video> videoList = new List<Video>();
        Mock<IData<Video>> repositoryVideo;
        Mock<IData<Category>> repositoryCategory;
        Mock<IData<Playlist>> repositoryPlaylist;
        VideoLogic videoLogic;
        List<Category> categoryList = new List<Category>();
        List<Playlist> playlistList = new List<Playlist>();
        Category category;
        Category secondCategory;

        [TestInitialize]
        public void Initialize()
        {
            category = new Category()
            {
                Id = 1,
                Name = "Dormir"
            };
            secondCategory = new Category()
            {
                Id = 2,
                Name = "Musica"
            };
            categoryList = new List<Category>();
            categoryList.Add(category);
            categoryList.Add(secondCategory);
            video = new Video()
            {
                Id = 0,
                Name = "Gasolina",
                Author = "Daddy yankee",
                LinkVideo = "www.youtube.com/daddyyanke/gasolina.mp3",
                Hour = 0,
                MinSeconds = 2.50,
                CategoryVideo = new List<CategoryVideo>(),
                PlaylistVideo = new List<PlaylistVideo>()
            };
            CategoryVideo categoryVideo = new CategoryVideo
            {
                IdCategory = 1
            };
            video.CategoryVideo.Add(categoryVideo);
            videoList.Add(video);
            repositoryVideo = new Mock<IData<Video>>();
            repositoryCategory = new Mock<IData<Category>>();
            repositoryPlaylist = new Mock<IData<Playlist>>();

            repositoryVideo.Setup(r => r.GetAll()).Returns(videoList);
            repositoryCategory.Setup(r => r.GetAll()).Returns(categoryList);
            repositoryPlaylist.Setup(r => r.GetAll()).Returns(playlistList);

            repositoryVideo.Setup(play => play.Get(0)).Returns(video);
            videoLogic = new VideoLogic(repositoryVideo.Object, repositoryCategory.Object, repositoryPlaylist.Object);
        }

        [TestMethod]
        public void GetVideo()
        {
            Video newVideo = videoLogic.Get(video.Id);
            Assert.AreEqual(video, newVideo);
        }

        [TestMethod]
        public void GetVideoNotExist()
        {
            Assert.ThrowsException<EntityNotExists>(() => videoLogic.Get(1));
        }

        [TestMethod]
        public void AddVideoOk()
        {
            Video videoToAdd = new Video()
            {
                Id = 1,
                Name = "Fiel",
                Author = "Wisin",
                LinkVideo = "www.youtube.com/fiel",
                Hour = 0,
                MinSeconds = 2.60,
                CategoryVideo = new List<CategoryVideo>(),
                PlaylistVideo = new List<PlaylistVideo>()
            };

            CategoryVideo categoryVideo = new CategoryVideo
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>(),
                    CategoryVideo = new List<CategoryVideo>()
                },
                IdCategory = 1,
                IdVideo = 1,
                Video = videoToAdd

            };
            videoToAdd.CategoryVideo.Add(categoryVideo);
            videoLogic.Add(videoToAdd);
        }

        [TestMethod]
        public void AddVideoNameEmpty()
        {
            Video videoToAdd = new Video()
            {
                Id = 1,
                Name = "",
                Author = "Wisin",
                LinkVideo = "www.youtube.com/fiel",
                Hour = 0,
                MinSeconds = 2.60,
                CategoryVideo = new List<CategoryVideo>(),
                PlaylistVideo = new List<PlaylistVideo>()
            };

            CategoryVideo categoryVideo = new CategoryVideo
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>(),
                    CategoryVideo = new List<CategoryVideo>()
                },
                IdCategory = 1,
                IdVideo = 1,
                Video = videoToAdd

            };
            videoToAdd.CategoryVideo.Add(categoryVideo);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => videoLogic.Add(videoToAdd));
        }

        [TestMethod]
        public void AddVideoLinkVideoEmpty()
        {
            Video videoToAdd = new Video()
            {
                Id = 1,
                Name = "Perreo",
                Author = "Wisin",
                LinkVideo = "",
                Hour = 0,
                MinSeconds = 2.60,
                CategoryVideo = new List<CategoryVideo>(),
                PlaylistVideo = new List<PlaylistVideo>()
            };

            CategoryVideo categoryVideo = new CategoryVideo
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dormir",
                    CategoryTrack = new List<CategoryTrack>(),
                    PlaylistCategory = new List<PlaylistCategory>(),
                    CategoryVideo = new List<CategoryVideo>()
                },
                IdCategory = 1,
                IdVideo = 1,
                Video = videoToAdd

            };
            videoToAdd.CategoryVideo.Add(categoryVideo);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => videoLogic.Add(videoToAdd));
        }
    }
}

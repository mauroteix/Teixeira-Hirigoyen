using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.DataAccessTest
{
    [TestClass]
    public class VideoRepositoryTest
    {
        List<Video> listVideo;
        VideoRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            listVideo = new List<Video>() {
                new Video()
                {
                    Id = 1,
                    Name = "Bailando",
                    Author = "Rodrigo",
                    Hour = 0,
                    MinSeconds = 1.25,
                    LinkVideo = "www.youtube.com/bailandorodrigo"
                },
                new Video()
                {
                    Id = 2,
                    Name = "Gasolina",
                    Author = "Mauro",
                    Hour = 2,
                    MinSeconds = 30.1,
                    LinkVideo = "www.youtube.com/gasolinamauro"

                }
            };
        }

        [TestMethod]
        public void AddVideo()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(listVideo[0]);
            context.SaveChanges();
            repository = new VideoRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(listVideo[1]);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }

        [TestMethod]
        public void GetOneVideo()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listVideo.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new VideoRepository(context);
            var video = repository.Get(listVideo[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listVideo[0].Id, video.Id);
        }
    }
}

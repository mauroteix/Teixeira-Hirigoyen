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
     public class TrackRepositoryTest
    {
        List<Track> listTrack;
        TrackRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            listTrack = new List<Track>() {
                new Track()
                {
                    Id = 1,
                    Name = "Los pibes chorros",
                    Image = "",
                    Hour = 0,
                    MinSeconds = 1.25


                },
                new Track()
                {
                    Id = 2,
                    Name = "Gasolina",
                    Image = "",
                     Hour = 2,
                    MinSeconds = 30.1

                }
            };
        }

        [TestMethod]
        public void AddTrack()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(listTrack[0]);
            context.SaveChanges();
            repository = new TrackRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(listTrack[1]);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }
        [TestMethod]
        public void GetOneTrack()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listTrack.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new TrackRepository(context);
            var track = repository.Get(listTrack[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listTrack[0].Id, track.Id);
        }
        [TestMethod]
        public void UpdateTrack()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listTrack.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new TrackRepository(context);
            listTrack[0].Name = "Pop";
            repository.Update(listTrack[0]);
            var track = repository.Get(listTrack[0].Id);
            context.Database.EnsureDeleted();

            Assert.AreEqual(track.Name, "Pop");
        }
    }
}

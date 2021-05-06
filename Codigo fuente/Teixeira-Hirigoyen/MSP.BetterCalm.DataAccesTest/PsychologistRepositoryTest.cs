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
    public class PsychologistRepositoryTest
    {
        Psychologist psychologist;
        Psychologist psychologistSecond;
        PsychologistRepository repository;
        List<Psychologist> listPsychologist;
        [TestInitialize]
        public void Initialize()
        {
            listPsychologist = new List<Psychologist>();
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                Meeting = new List<Meeting>(),

            };
            psychologistSecond = new Psychologist
            {
                Name = "Jorgito",
                Id = 2,
                MeetingType = meetingType.FaceToFace,
                Meeting = new List<Meeting>(),

            };
            listPsychologist.Add(psychologist);
            listPsychologist.Add(psychologistSecond);
        }
        [TestMethod]
        public void AddPsychologist()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
            .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            context.Add(psychologist);
            context.SaveChanges();
            repository = new PsychologistRepository(context);
            var initial = repository.GetAll().Count();

            repository.Add(psychologistSecond);
            var final = repository.GetAll().Count();
            context.Database.EnsureDeleted();

            Assert.AreEqual(initial + 1, final);
        }
        [TestMethod]
        public void GetOnePsychologist()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listPsychologist.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new PsychologistRepository(context);
            var playlist = repository.Get(listPsychologist[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listPsychologist[0].Id, playlist.Id);
        }
        [TestMethod]
        public void UpdatePsychologist()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listPsychologist.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new PsychologistRepository(context);
            listPsychologist[0].Name = "Pop";
            repository.Update(listPsychologist[0]);
            var psy = repository.Get(listPsychologist[0].Id);
            context.Database.EnsureDeleted();

            Assert.AreEqual(psy.Name, "Pop");
        }
        [TestMethod]
        public void DeleteOnePsychologist()
        {
            Psychologist playlistDelete = new Psychologist
            {
                Name = "Mauro",
                Id = 3,
                MeetingType = meetingType.Virtual,
                Meeting = new List<Meeting>(),
            };
            listPsychologist.Add(playlistDelete);
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listPsychologist.ForEach(playlist => context.Add(playlist));
            context.SaveChanges();
            repository = new PsychologistRepository(context);
            repository.Delete(playlistDelete);
            context.Database.EnsureDeleted();
            Psychologist getPsychologist = repository.Get(3);
            Assert.AreEqual(null, getPsychologist);
        }

    }
}

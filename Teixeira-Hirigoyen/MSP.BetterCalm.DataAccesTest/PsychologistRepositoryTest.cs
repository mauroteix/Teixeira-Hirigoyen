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
        [TestInitialize]
        public void Initialize()
        {
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                AdressMeeting = "Horacio 7895",
                Meeting = new List<Meeting>(),

            };
            psychologistSecond = new Psychologist
            {
                Name = "Jorgito",
                Id = 2,
                MeetingType = meetingType.FaceToFace,
                AdressMeeting = "Horacio 7895",
                Meeting = new List<Meeting>(),

            };
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

    }
}

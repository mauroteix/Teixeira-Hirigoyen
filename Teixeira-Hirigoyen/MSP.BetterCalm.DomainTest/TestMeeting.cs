using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestMeeting
    {
        User user;
        Meeting meeting;
        Psychologist psychologist;
        [TestInitialize]
        public void Initialize()
        {
            user = new User
            {
                Name = "Mauro",
                Id = 1,
                Surname = "Teixeira",
                Birthday = new DateTime(1996, 1, 1),
                Email = "mauroGil@gmail.com",
                Cellphone = "099156189",

            };
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                AdressMeeting = "Horacio 7895",
            };
            meeting = new Meeting
            {
                IdMeeting = 1,
                User = user,
                IdUser = user.Id,
                Psychologist = psychologist,
                IdPsychologist = psychologist.Id,
                Date = new DateTime(2021, 4, 18),
            };

        }
        [TestMethod]
        public void RegisterUser()
        {
            Assert.AreEqual(user, meeting.User);
        }
        [TestMethod]
        public void RegisterUserId()
        {
            Assert.AreEqual(user.Id, meeting.IdUser);
        }
        [TestMethod]
        public void RegisterPsychologist()
        {
            Assert.AreEqual(psychologist, meeting.Psychologist);
        }
        [TestMethod]
        public void RegisterPsychologistId()
        {
            Assert.AreEqual(psychologist.Id, meeting.IdPsychologist);
        }
        [TestMethod]
        public void RegisterDate()
        {
            DateTime date = new DateTime(2021, 4, 18);
            Assert.AreEqual(date, meeting.Date);
        }
    }
}

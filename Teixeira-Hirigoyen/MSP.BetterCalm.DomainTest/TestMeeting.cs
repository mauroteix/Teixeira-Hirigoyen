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
            meeting = new Meeting
            {
                IdMeeting = 1,
                User = user,
                IdUser = user.Id,
            };

        }
        [TestMethod]
        public void RegisterUser()
        {
            Assert.AreEqual(user, meeting.User);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestUserLogic
    {
        User user;
        List<User> userList = new List<User>();
        Mock<IData<User>> repositoryUser;
        UserLogic userLogic;
        PsychologistLogic psychologistLogic;
        [TestInitialize]
        public void Initialize()
        {
            user = new User()
            {
                Id = 1,
                Name = "Rodrigo",
                Surname = "Hirigoyen",
                Cellphone = "099925927",
                Email = "Hirigoyen@hotmail.com",
                Meeting = new List<Meeting>(),
                Birthday = new DateTime(2000,01,01),
            };

            userList = new List<User>();
            userList.Add(user);
            repositoryUser = new Mock<IData<User>>(); 
            repositoryUser.Setup(r => r.GetAll()).Returns(userList);
            userLogic = new UserLogic(repositoryUser.Object,psychologistLogic);
        }
        [TestMethod]
        public void AddUserOk()
        {
            userLogic.Add(user);
        }
        [TestMethod]
        public void AddUserNameEmpty()
        {
            user.Name = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserSurnameEmpty()
        {
            user.Surname = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserEmailEmpty()
        {
            user.Email = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserCellphoneEmpty()
        {
            user.Cellphone = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserMeetingNotEmpty()
        {
            Meeting meeting = new Meeting()
            {
                IdMeeting = 1,
                IdPsychologist = 1,
                IdUser = 1,
                Date = new DateTime(2018, 05, 15),
            };
            user.Meeting.Add(meeting);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
    }
}

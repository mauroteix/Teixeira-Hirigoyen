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
            userLogic = new UserLogic(repositoryUser.Object);
            repositoryUser.Setup(r => r.GetAll()).Returns(userList);
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
    }
}

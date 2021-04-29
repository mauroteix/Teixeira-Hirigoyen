﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Text;
namespace MSP.BetterCalm.APITest
{
    [TestClass]
    class UserControllerTest
    {
        List<User> userlistList;

        [TestInitialize]
        public void Initialize()
        {
            User user = new User
            {
                Name = "Mauro",
                Id = 1,
                Surname = "Teixeira",
                Birthday = new DateTime(1996, 1, 1),
                Email = "mauroGil@gmail.com",
                Cellphone = "099156189",

            };
            userlistList = new List<User>();
            userlistList.Add(user);
        }
        [TestMethod]
        public void AddOneUser()
        {
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(res => res.Add(userlistList[0]));
            UserController controller = new UserController(mockUser.Object);

            var result = controller.Add(userlistList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(userlistList[0]).ToString());
        }

    }
}
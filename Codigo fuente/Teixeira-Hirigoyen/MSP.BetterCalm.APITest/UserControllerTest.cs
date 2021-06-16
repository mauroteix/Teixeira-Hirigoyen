using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
namespace MSP.BetterCalm.APITest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserControllerTest
    {
        List<User> userList;

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
                MeetingDuration = meetingDuration.OneHour,
                MeetingCount = 0,
                Discount = discount.Zero,



            };
            userList = new List<User>();
            userList.Add(user);
        }
        [TestMethod]
        public void AddOneUser()
        {

            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(res => res.GetUserByEmail(userList[0].Email)).Returns(userList[0]);
            UserController controller = new UserController(mockUser.Object);

            var result = controller.Add(userList[0]);
            var okResult = result as OkObjectResult;

            var resultado = new ObjectResult("").ToString();
            var comp = controller.Add(userList[0]).ToString();
            Assert.AreEqual(resultado, comp);
        }

        [TestMethod]
        public void AddOneUserNameEmpty()
        {
            userList[0].Name = "";
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(res => res.Add(userList[0])).Throws(new FieldEnteredNotCorrect(""));
            UserController controller = new UserController(mockUser.Object);

            var result = controller.Add(userList[0]);
            var okResult = result as OkObjectResult;
            mockUser.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), controller.Add(userList[0]).ToString());
        }
        [TestMethod]
        public void AddOneUserSurnameEmpty()
        {
            userList[0].Surname = "";
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(res => res.Add(userList[0])).Throws(new FieldEnteredNotCorrect(""));
            UserController controller = new UserController(mockUser.Object);

            var result = controller.Add(userList[0]);
            var okResult = result as OkObjectResult;
            mockUser.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), controller.Add(userList[0]).ToString());
        }
        [TestMethod]
        public void AddOneUserCellphoneEmpty()
        {
            userList[0].Cellphone = "";
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(res => res.Add(userList[0])).Throws(new FieldEnteredNotCorrect(""));
            UserController controller = new UserController(mockUser.Object);

            var result = controller.Add(userList[0]);
            var okResult = result as OkObjectResult;
            mockUser.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), controller.Add(userList[0]).ToString());
        }
        [TestMethod]
        public void AddOneUserIncorrectEmail()
        {
            userList[0].Email = "asdasdasdasd";
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(res => res.Add(userList[0])).Throws(new FieldEnteredNotCorrect(""));
            UserController controller = new UserController(mockUser.Object);

            var result = controller.Add(userList[0]);
            var okResult = result as OkObjectResult;
            mockUser.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), controller.Add(userList[0]).ToString());
        }
        [TestMethod]
        public void UpdateUser()
        {
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(l => l.GetUserByEmail(userList[0].Email)).Returns(userList[0]);
            mockUser.Setup(l => l.Add(userList[0]));
            var controller = new UserController(mockUser.Object);
            userList[0].MeetingCount = 15;
            userList[0].Discount = discount.Fifteen;


            var result = controller.Update(userList[0].Id, userList[0]);
            Assert.AreEqual(new ObjectResult("Updated successfully").ToString(),
                result.ToString());
        }
        [TestMethod]
        public void GetUserByCountMeeting()
        {
            userList[0].MeetingCount = 8;
            var mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUser.Setup(l => l.GetUserByEmail(userList[0].Email)).Returns(userList[0]);
            mockUser.Setup(l => l.Add(userList[0]));
            var controller = new UserController(mockUser.Object);

            var result = controller.GetUserbyCountMeeting().ToString();
            Assert.AreEqual(new ObjectResult("").ToString(),
                result.ToString());
        }



    }
}

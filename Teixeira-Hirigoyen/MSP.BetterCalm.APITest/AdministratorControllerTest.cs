using Microsoft.AspNetCore.Mvc;
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
    public class AdministratorControllerTest
    {
        List<Administrator> adminList;

        [TestInitialize]
        public void Initialize()
        {
            Administrator admin = new Administrator()
            {
                Id = 1,
                Name = "Mauro",
                Password = "123456",
                Email = "mauro@hotmail.com"
            };
            adminList = new List<Administrator>();
            adminList.Add(admin);
        }

        [TestMethod]
        public void AddOneAdministrator()
        {
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(res => res.Get(adminList[0].Id)).Returns(adminList[0]);
            AdministratorController controller = new AdministratorController(mockAdmin.Object);

            var result = controller.Add(adminList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(adminList[0]).ToString());
        }

        [TestMethod]
        public void AddAdministratorError()
        {
            adminList[0].Name = "";
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(r => r.Add(adminList[0])).Throws(new FieldEnteredNotCorrect(""));
            AdministratorController controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Add(adminList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void DeleteAdministratorOk()
        {
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(t => t.Get(1)).Returns(adminList[0]);
            mockAdmin.Setup(t => t.Delete(adminList[0]));
            var controller = new AdministratorController(mockAdmin.Object);
            controller.Add(adminList[0]);
            var result = controller.Delete(1);
            Assert.AreEqual(new OkObjectResult("").ToString(),
                result.ToString());
        }

    }
}

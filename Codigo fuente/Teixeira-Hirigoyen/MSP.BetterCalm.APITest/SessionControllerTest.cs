using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msp.BetterCalm.HandleMessage;
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
    public class SessionControllerTest
    {

        [TestMethod]
        public void LoginAdminOk()
        {
            var admin = new Administrator()
            {
                Email = "mauro@hotmail.com",
                Password = "12345"
            };

            var tokenToReturn = Guid.NewGuid();

            var mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Login(It.IsAny<Administrator>())).Returns(tokenToReturn);
            var controller = new SessionController(mock.Object);

            var result = controller.Login(admin);
            var status = result as OkObjectResult;
            var content = status.Value.ToString();

            mock.VerifyAll();
            Assert.AreEqual(content, tokenToReturn.ToString());
        }

        [TestMethod]
        public void LoginAdminIncorrect()
        {
            var admin = new Administrator()
            {
                Email = "mauro@hotmail.com",
                Password = "12345"
            };

            var mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Login(It.IsAny<Administrator>())).Throws(new EntityNotExists("The login admin is incorrect")).ToString();
            var controller = new SessionController(mock.Object);

            var result = controller.Login(admin);
            var status = result as ObjectResult;
            var statusCode = status.StatusCode;
            var content = status.Value as string;

            mock.VerifyAll();
            Assert.AreEqual(statusCode, 404);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Controllers;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.APITest
{
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
    }
}

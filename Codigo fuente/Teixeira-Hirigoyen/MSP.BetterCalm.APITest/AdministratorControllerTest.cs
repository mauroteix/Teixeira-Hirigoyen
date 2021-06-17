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
            mockAdmin.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void AddAdministratorErrorEmail()
        {
            adminList[0].Email = "";
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(r => r.Add(adminList[0])).Throws(new FieldEnteredNotCorrect(""));
            AdministratorController controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Add(adminList[0]);
            mockAdmin.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void AddAdministratorErrorPassword()
        {
            adminList[0].Password = "";
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(r => r.Add(adminList[0])).Throws(new FieldEnteredNotCorrect(""));
            AdministratorController controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Add(adminList[0]);
            mockAdmin.VerifyAll();
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void AddAdministratorErrorEmailFormatWrong()
        {
            adminList[0].Email = "mauro";
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

        [TestMethod]
        public void DeleteAdministratorIdNegative()
        {
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            var controller = new AdministratorController(mockAdmin.Object);
            controller.Add(adminList[0]);
            var result = controller.Delete(-2);
            Assert.AreEqual(new NotFoundObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void DeleteAdministratorNotExists()
        {
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(l => l.Get(2)).Returns(adminList[0]);
            var controller = new AdministratorController(mockAdmin.Object);

            var result = controller.Delete(3);
            Assert.AreEqual(new ObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdateAdministrator()
        {
            Administrator newAdmin = new Administrator()
            {
                Name = "Rodri",
                Password = "0123"
            };
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(l => l.Get(adminList[0].Id)).Returns(adminList[0]);
            mockAdmin.Setup(l => l.Add(adminList[0]));
            var controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Update(adminList[0].Id, newAdmin);

            Assert.AreEqual(new ObjectResult("Updated successfully").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdateAdministratorNotFound()
        {
            Administrator newAdmin = new Administrator()
            {
                Name = "Rodri",
                Password = "0123"
            };
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(l => l.Get(adminList[0].Id)).Throws(new EntityNotExists("")); 
            mockAdmin.Setup(l => l.Add(adminList[0]));
            var controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Update(5, newAdmin);
            Assert.AreEqual(new ObjectResult("").ToString(), result.ToString());
          
        }

        [TestMethod]
        public void UpdateAdministratorError()
        {
            Administrator newAdmin = new Administrator()
            {
                Name = "",
                Password = "0123"
            };
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(l => l.Get(adminList[0].Id)).Throws(new FieldEnteredNotCorrect(""));
            mockAdmin.Setup(l => l.Add(adminList[0]));
            var controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Update(adminList[0].Id, newAdmin);

            Assert.AreEqual(new ObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void UpdateAdministratorErrorPassword()
        {
            Administrator newAdmin = new Administrator()
            {
                Name = "mauro",
                Password = ""
            };
            var mockAdmin = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdmin.Setup(l => l.Get(adminList[0].Id)).Throws(new FieldEnteredNotCorrect("")); ;
            mockAdmin.Setup(l => l.Add(adminList[0]));
            var controller = new AdministratorController(mockAdmin.Object);
            var result = controller.Update(adminList[0].Id, newAdmin);

            Assert.AreEqual(new ObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void GetAllAdministrator()
        {
            var mockAdministrator = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdministrator.Setup(u => u.GetAll()).Returns(adminList);
            var controller = new AdministratorController(mockAdministrator.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), controller.GetAll().ToString());
        }
        [TestMethod]
        public void GetOneAdministratorById()
        {
            var mockAdministrator = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdministrator.Setup(res => res.Get(adminList[0].Id)).Returns(adminList[0]);
            var controller = new AdministratorController(mockAdministrator.Object);
            var result = controller.Get(adminList[0].Id);

            mockAdministrator.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }

    }
}

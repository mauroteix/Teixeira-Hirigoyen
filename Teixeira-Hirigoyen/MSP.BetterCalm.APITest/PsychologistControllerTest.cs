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
    public class PsychologistControllerTest
    {
        List<Psychologist> psychologistList;

        [TestInitialize]
        public void Initialize()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                Name = "PEPE",
                Expertise= new List<Expertise>(),
                Meeting = new List<Meeting>()
            };
            psychologistList = new List<Psychologist>();
            psychologistList.Add(psychologist);
        }
        [TestMethod]
        public void GetOnePsychologist()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(res => res.Get(psychologistList[0].Id)).Returns(psychologistList[0]);
            PsychologistController controller = new PsychologistController(mockPsychologist.Object);
            var result = controller.Get(psychologistList[0].Id);

            mockPsychologist.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }
        [TestMethod]
        public void AddOnePsychologist()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(res => res.Get(psychologistList[0].Id)).Returns(psychologistList[0]);
            PsychologistController controller = new PsychologistController(mockPsychologist.Object);

            var result = controller.Add(psychologistList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(psychologistList[0]).ToString());
        }
        [TestMethod]
        public void AddPsychologistError()
        {
            psychologistList[0].Name = "";
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(r => r.Add(psychologistList[0])).Throws(new FieldEnteredNotCorrect(""));
            PsychologistController controller = new PsychologistController(mockPsychologist.Object);
            var result = controller.Add(psychologistList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }

        [TestMethod]
        public void AddPsychologistErrorEntity()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(r => r.Add(psychologistList[0])).Throws(new EntityNotExists(""));
            PsychologistController controller = new PsychologistController(mockPsychologist.Object);
            var result = controller.Add(psychologistList[0]);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(), result.ToString());
        }
        [TestMethod]
        public void DeletePsychologistOk()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(t => t.Get(1)).Returns(psychologistList[0]);
            mockPsychologist.Setup(t => t.Delete(psychologistList[0]));
            var controller = new PsychologistController(mockPsychologist.Object);
            controller.Add(psychologistList[0]);
            var result = controller.DeletePsychologist(1);
            Assert.AreEqual(new OkObjectResult("").ToString(),
                result.ToString());
        }
        [TestMethod]
        public void DeletePsychologistIdNegative()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            var controller = new PsychologistController(mockPsychologist.Object);
            controller.Add(psychologistList[0]);
            var result = controller.DeletePsychologist(-2);
            Assert.AreEqual(new NotFoundObjectResult("").ToString(),
                result.ToString());
        }
        [TestMethod]
        public void DeletePsychologistNotExists()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(l => l.Get(1)).Returns(psychologistList[0]);
            mockPsychologist.Setup(l => l.Delete(psychologistList[0]));
            var controller = new PsychologistController(mockPsychologist.Object);

            var result = controller.DeletePsychologist(3);
            Assert.AreEqual(new ObjectResult("").ToString(),
                result.ToString());
        }
        [TestMethod]
        public void UpdatePsychologist()
        {
            Psychologist newPsychologist = new Psychologist()
            {
                Name = "PEPErulo",
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()
            };
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(l => l.Get(psychologistList[0].Id)).Returns(psychologistList[0]);
            mockPsychologist.Setup(l => l.Add(psychologistList[0]));
            var controller = new PsychologistController(mockPsychologist.Object);
            psychologistList[0].Name = newPsychologist.Name;
            psychologistList[0].Expertise = newPsychologist.Expertise;
            psychologistList[0].Meeting = newPsychologist.Meeting;

            var result = controller.UpdatePsychologist(psychologistList[0].Id, psychologistList[0]);
            Assert.AreEqual(new ObjectResult("Updated successfully").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdatePsychologistNotExist()
        {
            Psychologist newPsychologist = new Psychologist()
            {
                Id = 1,
                Name = "PEPErulo",
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()
            };
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(l => l.Get(psychologistList[0].Id)).Returns(psychologistList[0]);
            mockPsychologist.Setup(l => l.Update(psychologistList[0],1)).Throws(new EntityNotExists(""));
            var controller = new PsychologistController(mockPsychologist.Object);
            var result = controller.UpdatePsychologist(1, newPsychologist);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

        [TestMethod]
        public void UpdatePsychologistNotCorrect()
        {
            Psychologist newPsychologist = new Psychologist()
            {
                Id = 1,
                Name = "",
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()
            };
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(l => l.Get(psychologistList[0].Id)).Returns(psychologistList[0]);
            mockPsychologist.Setup(l => l.Update(psychologistList[0], 1)).Throws(new FieldEnteredNotCorrect(""));
            var controller = new PsychologistController(mockPsychologist.Object);
            var result = controller.UpdatePsychologist(1, newPsychologist);
            Assert.AreEqual(new UnprocessableEntityObjectResult("").ToString(),
                result.ToString());
        }

    }
}

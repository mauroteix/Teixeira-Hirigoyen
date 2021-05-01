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
                AdressMeeting = "pepe 3251",
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
        public void AddOnePlaylist()
        {
            var mockPsychologist = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            mockPsychologist.Setup(res => res.Get(psychologistList[0].Id)).Returns(psychologistList[0]);
            PsychologistController controller = new PsychologistController(mockPsychologist.Object);

            var result = controller.Add(psychologistList[0]);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(new ObjectResult("").ToString(), controller.Add(psychologistList[0]).ToString());
        }
        [TestMethod]
        public void DeletePlaylistOk()
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

    }
}

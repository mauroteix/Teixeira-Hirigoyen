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

    }
}

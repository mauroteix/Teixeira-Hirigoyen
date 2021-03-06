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
    public class MedicalConditionControllerTest
    {
        List<MedicalCondition> medicalConditionList;
        [TestInitialize]
        public void Initialize()
        {
            MedicalCondition medicalCondition = new MedicalCondition()
            {
                Id = 1,
                Name = "Musica",
                Expertise = new List<Expertise>(),
            };
            medicalConditionList = new List<MedicalCondition>();
            medicalConditionList.Add(medicalCondition);
        }
        [TestMethod]
        public void GetOneMedicalConditionOk()
        {
            var mockMedicalCondition = new Mock<IMedicalConditionLogic>(MockBehavior.Strict);
            mockMedicalCondition.Setup(res => res.Get(medicalConditionList[0].Id)).Returns(medicalConditionList[0]);
            MedicalConditionController controller = new MedicalConditionController(mockMedicalCondition.Object);
            var result = controller.Get(medicalConditionList[0].Id);

            mockMedicalCondition.VerifyAll();
            Assert.AreEqual(result.ToString(), new OkObjectResult("").ToString());
        }

        [TestMethod]
        public void GetOneMedicalConditionError()
        {
            var mockMedicalCondition = new Mock<IMedicalConditionLogic>(MockBehavior.Strict);
            mockMedicalCondition.Setup(res => res.Get(medicalConditionList[0].Id)).Throws(new Exception());
            MedicalConditionController controller = new MedicalConditionController(mockMedicalCondition.Object);
            var result = controller.Get(medicalConditionList[0].Id);

            mockMedicalCondition.VerifyAll();
            Assert.AreEqual(result.ToString(), new ObjectResult("").ToString());
        }

        [TestMethod]
        public void GetAllMedicalCondition()
        {
            var mockMedicalCondition = new Mock<IMedicalConditionLogic>(MockBehavior.Strict);
            mockMedicalCondition.Setup(u => u.GetAll()).Returns(medicalConditionList);
            var controller = new MedicalConditionController(mockMedicalCondition.Object);
            Assert.AreEqual(new OkObjectResult("").ToString(), controller.GetAll().ToString());
        }

    }
}

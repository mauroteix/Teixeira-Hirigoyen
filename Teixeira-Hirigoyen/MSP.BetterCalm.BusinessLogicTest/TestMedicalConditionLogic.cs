using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using System.Collections.Generic;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestMedicalConditionLogic
    {
        MedicalCondition medicalCondition;
        MedicalCondition secondMedicalCondition;
        Mock<IData<MedicalCondition>> repository;
        MedicalConditionLogic medicalConditionLogic;
        List<MedicalCondition> medicalConditionList;
        [TestInitialize]
        public void Initialize()
        {
            medicalCondition = new MedicalCondition()
            {
                Id = 1,
                Name = "Dormir"
            };
            secondMedicalCondition = new MedicalCondition()
            {
                Id = 2,
                Name = "Musica"
            };
            medicalConditionList = new List<MedicalCondition>();
            medicalConditionList.Add(medicalCondition);
            medicalConditionList.Add(secondMedicalCondition);
            repository = new Mock<IData<MedicalCondition>>();
            repository.Setup(r => r.GetAll()).Returns(medicalConditionList);
            repository.Setup(play => play.Get(1)).Returns(medicalCondition);
            repository.Setup(play => play.Add(medicalCondition));
            medicalConditionLogic = new MedicalConditionLogic(repository.Object);
        }
        [TestMethod]
        public void GetAll()
        {
            List<MedicalCondition> list = medicalConditionLogic.GetAll();
            Assert.AreEqual(list.Count, 2);
        }
        [TestMethod]
        public void GetMedicalCondition()
        {
            MedicalCondition newMedicalCondition = medicalConditionLogic.Get(medicalCondition.Id);
            Assert.AreEqual(medicalCondition, newMedicalCondition);
        }
        [TestMethod]
        public void GetMedicalConditionNotExist()
        {
            Assert.ThrowsException<EntityNotExists>(() => medicalConditionLogic.Get(10));
        }
    }
}

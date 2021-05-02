using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestPsychologistLogic
    {
        Psychologist psychologist;
        List<Psychologist> psychologistList;
        Mock<IData<Psychologist>> repositoryPsychologist;
        PsychologistLogic psychologistLogic;
        List<MedicalCondition> medicalConditionList = new List<MedicalCondition>();
        MedicalConditionLogic medicalConditionLogic;
        Mock<IData<MedicalCondition>> repositoryMedicalCondition;

        [TestInitialize]
        public void Initialize()
        {
            psychologistList = new List<Psychologist>();
            List<Expertise> expertiseList =  new List<Expertise>();
            List<MedicalCondition> mcList = new List<MedicalCondition>();
            MedicalCondition medicalCondition1 = new MedicalCondition()
            {
                Id = 1,
                Name = "Depresion"
            };
            mcList.Add(medicalCondition1);
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()

            };
            Expertise expertise = new Expertise()
            {
                IdMedicalCondition = medicalCondition1.Id,
                MedicalCondition = medicalCondition1,
                IdPsychologist = psychologist.Id,
                Psychologist = psychologist
            };
            expertiseList.Add(expertise);
            psychologist.Expertise = expertiseList;
            psychologistList.Add(psychologist);
            
            repositoryPsychologist = new Mock<IData<Psychologist>>();
            repositoryMedicalCondition = new Mock<IData<MedicalCondition>>();

            repositoryMedicalCondition.Setup(py => py.GetAll()).Returns(mcList);
            repositoryPsychologist.Setup(r => r.GetAll()).Returns(psychologistList);
            repositoryPsychologist.Setup(py => py.Get(1)).Returns(psychologist);
            repositoryPsychologist.Setup(py => py.Add(psychologist));

            psychologistLogic = new PsychologistLogic(repositoryPsychologist.Object, repositoryMedicalCondition.Object);
            medicalConditionLogic = new MedicalConditionLogic(repositoryMedicalCondition.Object);
            



        }
        [TestMethod]
        public void GetPsychologist()
        {
            Psychologist newPsychologist = psychologistLogic.Get(psychologist.Id);
            Assert.AreEqual(psychologist, newPsychologist);
        }
        [TestMethod]
        public void GetAllPsychologist()
        {
            List<Psychologist> psyList = psychologistLogic.GetAll();
            Assert.AreEqual(psyList.Count, 1);
        }
        [TestMethod]
        public void AddPsychologistOk()
        {
            psychologistLogic.Add(psychologist);
        }
        [TestMethod]
        public void DeletePsychologist()
        {
            psychologistLogic.Delete(psychologist);
            var getLodg = psychologistLogic.Get(psychologist.Id);
        }
        [TestMethod]
        public void UpdatePsychologist()
        {
            
            psychologist.Name = "Pepe";
            psychologistLogic.Update(psychologist, psychologist.Id);
        }


    }
}

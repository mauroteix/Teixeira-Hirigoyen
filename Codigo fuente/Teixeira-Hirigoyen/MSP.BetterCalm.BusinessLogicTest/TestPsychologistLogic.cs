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
        List<MedicalCondition> medicalConditionList;
        MedicalConditionLogic medicalConditionLogic;
        Mock<IData<MedicalCondition>> repositoryMedicalCondition;
        List<User> userList = new List<User>();
        UserLogic userLogic;
        Mock<IData<User>> repositoryUser;
        User user;

        [TestInitialize]
        public void Initialize()
        {
            psychologistList = new List<Psychologist>();
            List<Expertise> expertiseList =  new List<Expertise>();
            medicalConditionList = new List<MedicalCondition>();
            MedicalCondition medicalCondition = new MedicalCondition()
            {
                Id = 1,
                Name = "Depresion"
            };
            medicalConditionList.Add(medicalCondition);
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                MeetingPrice = meetingPrice.UY1000,
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()

            };
            Expertise expertise = new Expertise()
            {
                IdMedicalCondition = medicalCondition.Id,
                MedicalCondition = medicalCondition,
                IdPsychologist = psychologist.Id,
                Psychologist = psychologist
            };
            user = new User()
            {
                Id = 1,
                Name = "Rodrigo",
                Surname = "Hirigoyen",
                Cellphone = "099925927",
                Email = "Hirigoyen@hotmail.com",
                Meeting = new List<Meeting>(),
                Birthday = new DateTime(2000, 01, 01),
                MedicalCondition = medicalCondition,
            };
           
            expertiseList.Add(expertise);
            psychologist.Expertise = expertiseList;
            medicalCondition.Expertise = expertiseList;
            psychologistList.Add(psychologist);
            userList.Add(user);

            repositoryPsychologist = new Mock<IData<Psychologist>>();
            repositoryMedicalCondition = new Mock<IData<MedicalCondition>>();
            repositoryUser = new Mock<IData<User>>();

            repositoryMedicalCondition.Setup(py => py.GetAll()).Returns(medicalConditionList);
            repositoryPsychologist.Setup(r => r.GetAll()).Returns(psychologistList);
            repositoryPsychologist.Setup(py => py.Get(1)).Returns(psychologist);
            repositoryUser.Setup(py => py.Get(1)).Returns(user);
            repositoryMedicalCondition.Setup(py => py.Get(1)).Returns(medicalCondition);
            repositoryPsychologist.Setup(py => py.Add(psychologist));

            
            psychologistLogic = new PsychologistLogic(repositoryPsychologist.Object, repositoryMedicalCondition.Object);
            medicalConditionLogic = new MedicalConditionLogic(repositoryMedicalCondition.Object);
            userLogic = new UserLogic(repositoryUser.Object, repositoryMedicalCondition.Object, repositoryPsychologist.Object);




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

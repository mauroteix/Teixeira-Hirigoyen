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

        [TestInitialize]
        public void Initialize()
        {
            psychologistList = new List<Psychologist>();
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                AdressMeeting = "Horacio 7895",
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()

            };
            
            psychologistList.Add(psychologist);
            
            repositoryPsychologist = new Mock<IData<Psychologist>>();

            repositoryPsychologist.Setup(r => r.GetAll()).Returns(psychologistList);
            repositoryPsychologist.Setup(py => py.Get(1)).Returns(psychologist);
            repositoryPsychologist.Setup(py => py.Add(psychologist));

            psychologistLogic = new PsychologistLogic(repositoryPsychologist.Object);


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
            Psychologist psy = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                AdressMeeting = "Horacio 7895",
                Expertise = new List<Expertise>(),
                Meeting = new List<Meeting>()
            };
            psychologistLogic.Add(psy);
        }


    }
}

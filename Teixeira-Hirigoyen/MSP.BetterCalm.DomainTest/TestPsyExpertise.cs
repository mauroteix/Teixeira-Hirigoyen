using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPsyExpertise
    {
        MedicalCondition medicalCondition;
        PsyExpertise psyExpertise;
        Psychologist psychologist;

        [TestInitialize]
        public void Initialize()
        {
            medicalCondition = new MedicalCondition
            {
                Name = "Depresion",
                Id = 1,
            };
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                AdressMeeting = "Horacio 7895",
            };
            psyExpertise = new PsyExpertise
            {
                IdMedicalCondition = medicalCondition.Id,
                MedicalCondition = medicalCondition,
                Psychologist = psychologist,
                IdPsychologist = psychologist.Id,
            };
         
        }
        [TestMethod]
        public void RegisterMedicalCondition()
        {
            Assert.AreEqual(psyExpertise.MedicalCondition, medicalCondition);
        }
        [TestMethod]
        public void RegisterMedicalConditionId()
        {
            Assert.AreEqual(psyExpertise.MedicalCondition.Id, medicalCondition.Id);
        }
        [TestMethod]
        public void RegisterPsychologist()
        {
            Assert.AreEqual(psyExpertise.Psychologist, psychologist);
        }
        [TestMethod]
        public void RegisterPsychologistId()
        {
            Assert.AreEqual(psyExpertise.IdPsychologist, psychologist.Id);
        }
    }
}

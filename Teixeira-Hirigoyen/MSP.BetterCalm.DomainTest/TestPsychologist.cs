using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestPsychologist
    {
        Psychologist psychologist;
        [TestInitialize]
        public void Initialize()
        {
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                Consultation = consultation.Virtual,
                AdressMeeting = "Horacio 7895",
            };


        }
        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Mauro", psychologist.Name);
        }
        [TestMethod]
        public void RegisterConsultationVirtual()
        {
            Assert.AreEqual(consultation.Virtual, psychologist.Consultation);
        }
        [TestMethod]
        public void RegisterConsultationFaceToFace()
        {
            psychologist.Consultation = consultation.FaceToFace;
            Assert.AreEqual(consultation.FaceToFace, psychologist.Consultation);
        }
        [TestMethod]
        public void RegisterAdressMeeting()
        {
            Assert.AreEqual("Horacio 7895", psychologist.AdressMeeting);
        }

    }
}

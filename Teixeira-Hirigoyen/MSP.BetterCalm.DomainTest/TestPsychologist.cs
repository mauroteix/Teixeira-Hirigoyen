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
        List<MedicalCondition> listMedicalCondition;
        MedicalCondition medicalCondition;

        [TestInitialize]
        public void Initialize()
        {
            listMedicalCondition = new List<MedicalCondition>();
            medicalCondition = new MedicalCondition
            {
                Name = "Depresion",
            };
            listMedicalCondition.Add(medicalCondition);
            psychologist = new Psychologist
            {
                Name = "Mauro",
                Id = 1,
                MeetingType = meetingType.Virtual,
                AdressMeeting = "Horacio 7895",
                ListMedicalCondition = null,
            };


        }
        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Mauro", psychologist.Name);
        }
        [TestMethod]
        public void RegisterMeetingTypeVirtual()
        {
            Assert.AreEqual(meetingType.Virtual, psychologist.MeetingType);
        }
        [TestMethod]
        public void RegisterMeetingTypeFaceToFace()
        {
            psychologist.MeetingType = meetingType.FaceToFace;
            Assert.AreEqual(meetingType.FaceToFace, psychologist.MeetingType);
        }
        [TestMethod]
        public void RegisterAdressMeeting()
        {
            Assert.AreEqual("Horacio 7895", psychologist.AdressMeeting);
        }
        [TestMethod]
        public void RegisterListMedicalCondition()
        {
            psychologist.ListMedicalCondition = listMedicalCondition;
            Assert.IsTrue(psychologist.ListMedicalCondition.Contains(listMedicalCondition[0]));
        }
    }
}

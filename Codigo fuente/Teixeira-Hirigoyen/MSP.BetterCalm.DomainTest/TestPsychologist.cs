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
                MeetingType = meetingType.Virtual,
                Expertise = new List<Expertise>()

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
        public void NameEmpty()
        {
            psychologist.Name = "";
            Assert.IsTrue(psychologist.NameEmpty());
        }
        [TestMethod]
        public void ExpertiseEmpty()
        {
            Assert.IsTrue(psychologist.ExpertiseEmpty());
        }
        [TestMethod]
        public void AdressMeetingEmpty()
        {
            psychologist.AdressMeeting = "";
            Assert.IsTrue(psychologist.AdressMeetingEmpty());
        }

    }
}

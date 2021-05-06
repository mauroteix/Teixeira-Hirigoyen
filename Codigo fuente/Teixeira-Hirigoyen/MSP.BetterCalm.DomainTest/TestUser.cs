using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestUser
    {
        User user;
        [TestInitialize]
        public void Initialize()
        {
            user = new User
            {
                Name = "Mauro",
                Id = 1,
                Surname = "Teixeira",
                Birthday = new DateTime(1996, 1, 1),
                Email = "mauroGil@gmail.com",
                Cellphone = "099156189",
                Meeting = new List<Meeting>(),
                MedicalCondition = new MedicalCondition()
                {
                    Id = 1,
                    Name = "Pepe"
                }

            };
            

        }
        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Mauro", user.Name);
        }
        [TestMethod]
        public void RegisterSurname()
        {
            Assert.AreEqual("Teixeira", user.Surname);
        }
        [TestMethod]
        public void RegisterBirthday()
        {
            DateTime date = new DateTime(1996, 1, 1);
            Assert.AreEqual(date, user.Birthday);
        }
        [TestMethod]
        public void RegisterEmail()
        {
            Assert.AreEqual("mauroGil@gmail.com", user.Email);
        }
        [TestMethod]
        public void RegisterCellphone()
        {
            Assert.AreEqual("099156189", user.Cellphone);
        }
        [TestMethod]
        public void RegisterMedicalCondition()
        {
            MedicalCondition mc = new MedicalCondition()
            {
                Id = 1,
                Name = "Pepe"
            };
            Assert.AreEqual(mc, user.MedicalCondition);
        }
        [TestMethod]
        public void NameEmpty()
        {
            user.Name = "";
            Assert.IsTrue(user.NameEmpty());
        }
        [TestMethod]
        public void SurnameEmpty()
        {
            user.Surname = "";
            Assert.IsTrue(user.SurnameEmpty());
        }
        [TestMethod]
        public void CellphoneEmpty()
        {
            user.Cellphone = "";
            Assert.IsTrue(user.CellphoneEmpty());
        }
        [TestMethod]
        public void MeetingEmpty()
        {
            Assert.IsTrue(user.MeetingEmpty());
        }
        [TestMethod]
        public void MedicalConditionEmpty()
        {
            user.MedicalCondition = null;

            Assert.IsTrue(user.MedicalConditionEmpty());
        }
    }
}

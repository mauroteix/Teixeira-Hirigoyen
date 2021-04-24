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
        [TestInitialize]
        public void Initialize()
        {
            medicalCondition = new MedicalCondition
            {
                Name = "Depresion",
                Id = 1,
            };
            psyExpertise = new PsyExpertise
            {
                IdMedicalCondition = medicalCondition.Id,
                MedicalCondition = medicalCondition,
            };
        }
        [TestMethod]
        public void RegisterMedicalCondition()
        {
            Assert.AreEqual(psyExpertise.MedicalCondition, medicalCondition);
        }
    }
}

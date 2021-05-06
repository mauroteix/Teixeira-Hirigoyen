using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MSP.BetterCalm.Domain;

namespace MSP.BetterCalm.DomainTest
{
    [TestClass]
    public class TestMedicalCondition
    {
        MedicalCondition medicalCondition;

        [TestMethod]
        public void RegisterName()
        {
            medicalCondition = new MedicalCondition();
            medicalCondition.Name = "Depresion";
            Assert.AreEqual("Depresion", medicalCondition.Name);
        }
    }
}

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
            };


        }
        [TestMethod]
        public void RegisterName()
        {
            Assert.AreEqual("Mauro", psychologist.Name);
        }

    }
}

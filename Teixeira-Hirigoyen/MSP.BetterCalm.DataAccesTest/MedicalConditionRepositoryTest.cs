using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSP.BetterCalm.DataAccess;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System.Collections.Generic;
using System.Linq;


namespace MSP.BetterCalm.DataAccessTest
{
   
    [TestClass]
    public class MedicalConditionRepositoryTest
    {
        List<MedicalCondition> listMedicalCondition;
        MedicalConditionRepository repository;
        [TestInitialize]
        public void Initialize()
        {
            listMedicalCondition = new List<MedicalCondition>() {
                new MedicalCondition()
                {
                    Id = 1,
                    Name = "Depresion",
                    Expertise  = new List<Expertise>()
                },
                new MedicalCondition()
                {
                    Id=2,
                    Name="Angustia",
                    Expertise  = new List<Expertise>()
                }
            };
        }
        [TestMethod]
        public void GetOneMedicalCondition()
        {
            var options = new DbContextOptionsBuilder<BetterCalmContext>()
                .UseInMemoryDatabase(databaseName: "MSP.BetterCalmDatabase").Options;
            var context = new BetterCalmContext(options);
            listMedicalCondition.ForEach(cat => context.Add(cat));
            context.SaveChanges();
            repository = new MedicalConditionRepository(context);
            var category = repository.Get(listMedicalCondition[0].Id);
            context.Database.EnsureDeleted();
            Assert.AreEqual(listMedicalCondition[0].Id, category.Id);
        }
    }
}

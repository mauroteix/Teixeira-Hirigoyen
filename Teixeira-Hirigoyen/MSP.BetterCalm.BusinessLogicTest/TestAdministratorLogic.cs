using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestAdministratorLogic
    {

        Administrator admin;
        List<Administrator> adminList = new List<Administrator>();
        Mock<IData<Administrator>> repositoryAdmin;
        //Mock<IData<Category>> repositoryCategory;
       // Mock<IData<Track>> repositoryTrack;
        AdministratorLogic adminLogic;
       // List<Category> categoryList = new List<Category>();
       // List<Track> trackList = new List<Track>();
       // Category category;
       // Category secondCategory;

        [TestInitialize]
        public void Initialize()
        {
            admin = new Administrator()
            {
                Id = 1,
                Name = "Mauro",
                Email = "mauro@hotmail.com",
                Password = "12345"
            };
            adminList = new List<Administrator>();
            adminList.Add(admin);
            
           
            repositoryAdmin = new Mock<IData<Administrator>>();
            //repositoryCategory = new Mock<IData<Category>>();
            //repositoryTrack = new Mock<IData<Track>>();

            repositoryAdmin.Setup(r => r.GetAll()).Returns(adminList);
            /*
            repositoryCategory.Setup(r => r.GetAll()).Returns(categoryList);
            repositoryTrack.Setup(r => r.GetAll()).Returns(trackList);
            */
            repositoryAdmin.Setup(play => play.Get(1)).Returns(admin);
            
            repositoryAdmin.Setup(play => play.Add(admin));
            adminLogic = new AdministratorLogic(repositoryAdmin.Object);

        }

        [TestMethod]
        public void GetAdministrator()
        {
            Administrator newAdministrator = adminLogic.Get(admin.Id);
            Assert.AreEqual(admin, newAdministrator);
        }
    }
}

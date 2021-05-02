using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.BusinessLogic;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;

namespace MSP.BetterCalm.BusinessLogicTest
{
    [TestClass]
    public class TestUserLogic
    {
        User user;
        List<User> userList = new List<User>();
        Mock<IData<User>> repositoryUser;
        UserLogic userLogic;
        PsychologistLogic psychologistLogic;
        Mock<IData<MedicalCondition>> repositoryMedical;
        Mock<IData<Psychologist>> repositoryPsychologist;
        MedicalCondition medicalCondition;
        List<MedicalCondition> listMedical = new List<MedicalCondition>();
        Psychologist psychologist;
        List<Psychologist> listPsychologist = new List<Psychologist>();
        [TestInitialize]
        public void Initialize()
        {
            Expertise expertise = new Expertise();
            medicalCondition = new MedicalCondition()
            {
                Id = 1,
                Name = "Depresion",
                Expertise = new List<Expertise>(),
            };

            psychologist = new Psychologist()
            {
                Id = 1,
                Name = "PEPE",
                MeetingType = meetingType.Virtual,
                Meeting = new List<Meeting>(),
                Expertise = new List<Expertise>()
                {
                    new Expertise()
                    {
                        MedicalCondition = medicalCondition,
                        IdMedicalCondition = 1,
                        Psychologist = psychologist,
                        IdPsychologist = 1
                    },
                }
            };
            expertise.MedicalCondition = medicalCondition;
            expertise.IdMedicalCondition = 1;
            expertise.IdPsychologist = 1;
            expertise.Psychologist = psychologist;
            medicalCondition.Expertise.Add(expertise);

       
            user = new User()
            {
                Id = 1,
                Name = "Rodrigo",
                Surname = "Hirigoyen",
                Cellphone = "099925927",
                Email = "Hirigoyen@hotmail.com",
                Meeting = new List<Meeting>(),
                Birthday = new DateTime(2000, 01, 01),
                MedicalCondition = medicalCondition,
            };
           

            userList = new List<User>();
            listMedical.Add(medicalCondition);
            listPsychologist.Add(psychologist);
            userList.Add(user);
            repositoryUser = new Mock<IData<User>>();
            repositoryPsychologist = new Mock<IData<Psychologist>>();
            repositoryMedical = new Mock<IData<MedicalCondition>>();
            repositoryUser.Setup(r => r.GetAll()).Returns(userList);
            repositoryPsychologist.Setup(r => r.Add(psychologist));
            repositoryMedical.Setup(r => r.GetAll()).Returns(listMedical);
            repositoryMedical.Setup(r => r.Get(medicalCondition.Id)).Returns(medicalCondition);
            repositoryPsychologist.Setup(r => r.GetAll()).Returns(listPsychologist);
            repositoryPsychologist.Setup(r => r.Get(psychologist.Id)).Returns(psychologist);

            psychologistLogic = new PsychologistLogic(repositoryPsychologist.Object,repositoryMedical.Object);
            userLogic = new UserLogic(repositoryUser.Object,psychologistLogic);
        }
        [TestMethod]
        public void AddUserOk()
        {

            userLogic.Add(user);
        }
        [TestMethod]
        public void AddUserNameEmpty()
        {
            user.Name = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserSurnameEmpty()
        {
            user.Surname = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserEmailEmpty()
        {
            user.Email = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserCellphoneEmpty()
        {
            user.Cellphone = "";
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
        [TestMethod]
        public void AddUserMeetingNotEmpty()
        {
            Meeting meeting = new Meeting()
            {
                IdPsychologist = 1,
                IdUser = 1,
                Date = new DateTime(2018, 05, 15),
            };
            user.Meeting.Add(meeting);
            Assert.ThrowsException<FieldEnteredNotCorrect>(() => userLogic.Add(user));
        }
    }
}

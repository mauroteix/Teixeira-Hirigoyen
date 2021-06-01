using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MSP.BetterCalm.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        IData<Psychologist> _repository;
        IData<User> _repositoryUser;
        IPsychologistLogic _logicPsychologist;
        IData<MedicalCondition> _repositoryMedicalCondition;

        public UserLogic(IData<User> repositoryUser, IPsychologistLogic logicPsychologist, IData<MedicalCondition> repositoryMedicalCondition, IData<Psychologist> repository)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
            _logicPsychologist = logicPsychologist;
            _repositoryMedicalCondition = repositoryMedicalCondition;

        }
        public void Add(User user)
        {
            ValidateUser(user);
            User newUser = CreateMeeting(user);
            _repositoryUser.Add(newUser);
        }
        public void ValidateUser(User user)
        {
            if (user.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if (user.SurnameEmpty()) throw new FieldEnteredNotCorrect("The surname cannot be empty");
            if (user.CellphoneEmpty()) throw new FieldEnteredNotCorrect("The cellphone cannot be empty");
            if (!user.MeetingEmpty()) throw new FieldEnteredNotCorrect("The meeting has to be empty");
            Regex regexEmail = new Regex(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$");
            if (!regexEmail.IsMatch(user.Email)) throw new FieldEnteredNotCorrect("Incorrect email it must have this form: asdasd@hotmail.com");
            if (!ValidateMeetingDuration(user)) throw new FieldEnteredNotCorrect("Only 3 types of meetingDuration");
        }
        private bool ValidateMeetingDuration(User user)
        {
            int valor = (int)user.MeetingDuration;
            if (valor > 3 && valor < 1) return false;
            return true;
        }
        private User CreateMeeting(User user)
        {
            user.Meeting = new List<Meeting>();
            var medicalCondition = _repositoryMedicalCondition.Get(user.MedicalCondition.Id);
            if (medicalCondition == null) throw new EntityNotExists("This medical condition not exist");
            user.MedicalCondition = medicalCondition;
            var date = DateTime.Now;
            date = ChangeDate(date);
            var list = ListOfPsychologist(medicalCondition);
            if (list.Count == 0) throw new EntityNotExists("There are no psychologist for this medical condition");
            Meeting meeting = new Meeting();
            Psychologist unPsychologist = FreePsychologist(list, date, meeting);

            meeting.IdUser = user.Id;
            meeting.User = user;
            meeting.Psychologist = unPsychologist;
            meeting.IdPsychologist = unPsychologist.Id;
            meeting.MeetingDuration = user.MeetingDuration;
            meeting.MeetingDiscount = user.Discount;
            //Falta metodo para total price aplicar descuento ----------------------------------------------
            meeting.TotalPrice = 0;

            meeting.AdressMeeting = CreateAdress(unPsychologist);
            user.Meeting.Add(meeting);
            return user;
        }

        private Psychologist FreePsychologist(List<Psychologist> list, DateTime date, Meeting meeting)
        {
            var listFreePsy = ListFreePsychologist(list, date);
            Psychologist psychologist = new Psychologist();
            while (listFreePsy.Count == 0)
            {
                date = date.AddDays(1);
                date = ChangeDate(date);
                listFreePsy = ListFreePsychologist(list, date);
            }
            if (listFreePsy.Count > 1)
            {
                psychologist = SelectOlderPsychologist(listFreePsy);
            }
            else
            {
                psychologist = listFreePsy[0];
            }
            psychologist = _repository.Get(psychologist.Id);
            meeting.Date = date;
            return psychologist;
        }
        public string CreateAdress(Psychologist psychologist)
        {
            string adress = "";
            Guid guid = Guid.NewGuid();
            if ((int)psychologist.MeetingType == 1)
            {
                adress = "https://bettercalm.com.uy/meeting/";
                var valor = guid;
                adress = adress + valor;
            }
            else
            {
                adress = psychologist.AdressMeeting;
            }

            return adress;
        }
        private DateTime ChangeDate(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }
        private List<Psychologist> ListOfPsychologist(MedicalCondition medicalCondition)
        {
            var list = medicalCondition.Expertise.ToList();
            List<Psychologist> listPsychologist = new List<Psychologist>();
            if (medicalCondition.Name.Equals("Otros")) return _repository.GetAll().ToList();

            list.ForEach(c => listPsychologist.Add(_repository.Get(c.Psychologist.Id)));
            return listPsychologist;
        }
        private List<Psychologist> ListFreePsychologist(List<Psychologist> listPsychologist, DateTime date)
        {
            var list = new List<Psychologist>();
            listPsychologist.ForEach(c =>
            {
                if (c.Meeting == null) c.Meeting = new List<Meeting>();
                if (IsFreeForMeeting(c, date)) list.Add(c);

            });
            return list;
        }
        private bool IsFreeForMeeting(Psychologist psychologist, DateTime date)
        {
            var list = psychologist.Meeting.ToList();
            var listMeetingDate = new List<Meeting>();
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            list.ForEach(c =>
            {
                if (c.Date.Year == year && c.Date.Month == month && c.Date.Day == day) listMeetingDate.Add(c);
            });

            return listMeetingDate.Count < 6;
        }
        private Psychologist SelectOlderPsychologist(List<Psychologist> list)
        {
            int position = 0;
            int minId = list[0].Id;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id < minId)
                {
                    position = i;
                    minId = list[i].Id;
                }
            }
            return list[position];
        }
        private double CalculateDiscount(double price, int discounttoapply)
        {
            double realprice = 0;
            if(discounttoapply == 1)
            {
                realprice = price*0.85;
            }
            if (discounttoapply == 2)
            {
                realprice = price * 0.75;
            }
            if (discounttoapply == 3)
            {
                realprice = price * 0.50;
            }
            return realprice;
        }

    }
}

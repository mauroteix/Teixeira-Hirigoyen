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

        public UserLogic(IData<User> repositoryUser, IData<MedicalCondition> repositoryMedicalCondition, IData<Psychologist> repository)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
            _repositoryMedicalCondition = repositoryMedicalCondition;

        }
        public void Add(User user)
        {
            ValidateUser(user);
            CreateMeeting(user);
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
            if (!ExistMedicalCondition(user)) throw new EntityNotExists("This medical condition not exist");
            if (!ValidateDiscount(user)) throw new FieldEnteredNotCorrect("Incorrect Discount try again");
        }
        private bool ValidateMeetingDuration(User user)
        {
            int valor = (int)user.MeetingDuration;
            if (valor > 3 && valor < 1) return false;
            return true;
        }
        private bool ValidateDiscount(User user)
        {
            int valor = (int)user.Discount;
            if (valor != 100 && valor != 15 && valor != 25 && valor != 50) return false;
            return true;
        }
        private void SetMeeting (User user , Psychologist unPsychologist,Meeting meeting)
        {
            meeting.IdUser = user.Id;
            meeting.User = user;
            meeting.Psychologist = unPsychologist;
            meeting.IdPsychologist = unPsychologist.Id;
            meeting.MeetingDuration = user.MeetingDuration;
            meeting.MeetingDiscount = user.Discount;
            meeting.TotalPrice = CreateDiscount(user, unPsychologist);
            meeting.AdressMeeting = CreateAdress(unPsychologist);
            meeting.Id = user.Meeting.Count;
        }
        private void SetMeetingCount(User user)
        {
            if((int)user.Discount != 100)
            {
                user.Discount = discount.Zero;
                user.MeetingCount = 0;
            }
            else
            {
                user.MeetingCount++;
            }
        }
        private bool ExistMedicalCondition(User user)
        {
            var medicalCondition = _repositoryMedicalCondition.Get(user.MedicalCondition.Id);
            if (medicalCondition == null) return false;
            return true;
        }
        private void CreateMeeting(User user)
        {
            user.Meeting = new List<Meeting>();
            var medicalCondition = _repositoryMedicalCondition.Get(user.MedicalCondition.Id);
            user.MedicalCondition = medicalCondition;
            var date = DateTime.Now;
            date = ChangeDate(date);
            var list = ListOfPsychologist(medicalCondition);
            Meeting meeting = new Meeting();
            Psychologist unPsychologist = FreePsychologist(list, date, meeting);
            if (ExistUser(user))
            {
                User newuser = _repositoryUser.Get(UserId(user));
                newuser.MeetingDuration = user.MeetingDuration;
                newuser.Discount = user.Discount;
                SetMeeting(newuser, unPsychologist, meeting);
                SetMeetingCount(newuser);
                newuser.Meeting.Add(meeting);
                user.Meeting = newuser.Meeting;
                UpdateByUser(newuser,newuser.Id);
            }
            else
            {
                SetMeeting(user, unPsychologist, meeting);
                SetMeetingCount(user);
                user.Meeting.Add(meeting);
                _repositoryUser.Add(user);
            }

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
            if (list.Count == 0) throw new EntityNotExists("There are no psychologist for this medical condition");
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

            return listMeetingDate.Count < 5;
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
        private int CreateDiscount(User user , Psychologist psy)
        {
            int price = (int)psy.MeetingPrice;
            int duration = (int)user.MeetingDuration;
            int userDiscount = (int)user.Discount;
            double realPrice = CalculatePrice(price, duration);
            double priceDiscount = CalculateDiscount(realPrice, userDiscount);
            
            return (int)priceDiscount;
        }
        private double CalculateDiscount(double price, int discounttoapply)
        {
            double discount = (double)discounttoapply /100;
            return price * discount;
        }
        private double CalculatePrice(int price,int duration)
        {
            if (duration == 3) return price * 1.5;
            return price * duration;
        }
        private bool ExistUser(User user)
        {
            List<User> list = _repositoryUser.GetAll().ToList();
            string email = user.Email;
            User findUser = list.Find(c => c.Email == email);
            if (findUser == null) return false; 
            return true;
        }
        private int UserId(User user)
        {
            List<User> list = _repositoryUser.GetAll().ToList();
            string email = user.Email;
            User findUser = list.Find(c => c.Email == email);
            return findUser.Id;
        }
        public void UpdateByAdministrator(User user, int id)
        {
            User realUser = _repositoryUser.Get(id);
            realUser.Meeting = user.Meeting;
            realUser.MeetingDuration = user.MeetingDuration;
            realUser.Discount = user.Discount;
            user.MeetingCount = 0;
            realUser.MeetingCount = user.MeetingCount;
            _repositoryUser.Update(realUser);
        }
        private void UpdateByUser(User user, int id)
        {
            User realUser = _repositoryUser.Get(id);
            realUser.Meeting = user.Meeting;
            realUser.MeetingDuration = user.MeetingDuration;
            realUser.Discount = user.Discount;
            realUser.MeetingCount = user.MeetingCount;
            _repositoryUser.Update(realUser);
        }
        public List<User> GetUserbyCountMeeting()
        {
            List<User> list = new List<User>();
            List<User> listAll = _repositoryUser.GetAll().ToList();
            listAll.ForEach(c => {
                if (c.MeetingCount > 4) list.Add(c);
            });
            return list;
        }
        public User GetUserByEmail(string email)
        {
            User newUser = new User();
            newUser.Email = email;
            if (ExistUser(newUser))
            {
                newUser = _repositoryUser.Get(UserId(newUser));
            }
            else
            {
                throw new EntityNotExists("There are no user with this email");
            }
            return newUser;
        }
    }
}

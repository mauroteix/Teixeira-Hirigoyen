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
        IData<User> _repository;
        IPsychologistLogic _logicPsychologist;

        public UserLogic(IData<User> repository, IPsychologistLogic logicPsychologist)
        {
            _repository = repository;
            _logicPsychologist = logicPsychologist;

        }
        public void Add(User user)
        {
            ValidateUser(user);
            User newUser = _logicPsychologist.CreateMeeting(user);
            _repository.Add(newUser);
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

    }
}

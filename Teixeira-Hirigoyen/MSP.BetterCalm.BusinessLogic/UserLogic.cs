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
        public UserLogic(IData<User> repository)
        {
            _repository = repository;
        }
        public void Add(User user)
        {
            if(user.NameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if(user.SurnameEmpty()) throw new FieldEnteredNotCorrect("The surname cannot be empty");
            if(user.CellphoneEmpty()) throw new FieldEnteredNotCorrect("The cellphone cannot be empty");
            if (!user.MeetingEmpty()) throw new FieldEnteredNotCorrect("The meeting has to be empty");
            Regex regexEmail = new Regex(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$");
            if (!regexEmail.IsMatch(user.Email)) throw new FieldEnteredNotCorrect("Incorrect email it must have this form: asdasd@hotmail.com");

            _repository.Add(user);
        }
    }
}

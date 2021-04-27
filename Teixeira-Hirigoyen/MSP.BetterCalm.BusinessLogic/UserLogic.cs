using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            if(user.SurnameEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            if(user.CellphoneEmpty()) throw new FieldEnteredNotCorrect("The name cannot be empty");
            _repository.Add(user);
        }
    }
}

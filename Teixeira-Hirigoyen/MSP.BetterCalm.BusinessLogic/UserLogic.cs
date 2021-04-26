using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.DTO;
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
            _repository.Add(user);
        }
    }
}

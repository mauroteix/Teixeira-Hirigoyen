using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IUserLogic
    {
        public void Add(User user);
        public void Update(User user, int id);
        public List<User> GetUserbyCountMeeting();
        public User GetUserByEmail(string email);
    }
}

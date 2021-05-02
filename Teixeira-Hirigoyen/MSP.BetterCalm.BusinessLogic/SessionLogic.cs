using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IData<Administrator> repositoryAdministrator;

        public SessionLogic(IData<Administrator> repository)
        {
            this.repositoryAdministrator = repository;
        }
        public bool IsCorrectToken(Guid token)
        {
            return this.repositoryAdministrator.GetAll().ToList().Exists(u => u.Token == token);
        }

        public Guid Login(Administrator admin)
        {
            if (admin.Email == null || admin.Password == null)
            {
                throw  new FieldEnteredNotCorrect("The email and password cannot be empty");
            }
            var adminLog = this.repositoryAdministrator.GetAll().ToList().FirstOrDefault(u =>
                u.Email.ToLower().Equals(admin.Email.ToLower())
                && u.Password.Equals(admin.Password));

            if (adminLog == null)
            {
                throw new FieldEnteredNotCorrect("The login admin is incorrect");
            }

            return adminLog.Token;
        }


    }
}

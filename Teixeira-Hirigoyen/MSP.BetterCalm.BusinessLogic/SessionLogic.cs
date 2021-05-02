using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
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


    }
}

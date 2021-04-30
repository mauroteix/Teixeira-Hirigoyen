using Msp.BetterCalm.HandleMessage;
using MSP.BetterCalm.BusinessLogicInterface;
using MSP.BetterCalm.DataAccessInterface;
using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        IData<Administrator> repositoryAdministrator;
        public AdministratorLogic(IData<Administrator> repository)
        {
            repositoryAdministrator = repository;
        }

        public Administrator Get(int id)
        {
            ExistAdministrator(id);
            return repositoryAdministrator.Get(id);
        }

        private void ExistAdministrator(int id)
        {
            Administrator unAdmin = repositoryAdministrator.Get(id);
            if (unAdmin == null) throw new EntityNotExists("The admin with id: " + id + " does not exist");
        }
    }
}

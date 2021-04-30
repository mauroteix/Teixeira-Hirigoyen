using MSP.BetterCalm.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface IAdministratorLogic
    {
        public Administrator Get(int id);
    }
}

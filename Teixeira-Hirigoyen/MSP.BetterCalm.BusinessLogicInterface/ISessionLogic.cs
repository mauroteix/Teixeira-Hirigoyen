using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.BusinessLogicInterface
{
    public interface ISessionLogic
    {
        public bool IsCorrectToken(Guid token);
    }
}

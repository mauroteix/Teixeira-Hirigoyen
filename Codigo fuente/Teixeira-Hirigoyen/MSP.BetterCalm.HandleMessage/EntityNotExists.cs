using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Msp.BetterCalm.HandleMessage
{
    public class EntityNotExists : ExceptionError
    {
        string message;
        public EntityNotExists(string error)
        {
            message = error;
        }
        public override string MessageError()
        {
            return message;
        }
    }
}

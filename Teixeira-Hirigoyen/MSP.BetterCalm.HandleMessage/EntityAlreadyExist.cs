using MSP.BetterCalm.HandleMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace UruguayNatural.HandleError
{
    public class EntityAlreadyExist : ExceptionError
    {
        string message;
        public EntityAlreadyExist(string error)
        {
            message = error;
        }
        public override string MessageError()
        {
            return message + ". Please try again.";
        }
    }
}

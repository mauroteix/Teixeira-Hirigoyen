using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.HandleMessage
{
    public class CannotBePerformed : ExceptionError
    {
        string message;
        public CannotBePerformed(string error)
        {
            message = error;
        }

        public override string MessageError()
        {
            return message + ". Please try again!.";
        }
    }
}

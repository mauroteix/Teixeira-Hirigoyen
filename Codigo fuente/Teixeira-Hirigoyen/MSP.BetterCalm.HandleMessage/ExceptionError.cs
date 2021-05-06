using System;

namespace MSP.BetterCalm.HandleMessage
{
    public abstract class ExceptionError : Exception
    {
        public abstract string MessageError();
    }
}

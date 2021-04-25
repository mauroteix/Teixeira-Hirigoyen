namespace MSP.BetterCalm.HandleMessage
{
    public class FieldEnteredNotCorrect: ExceptionError
    {
        string message;
        public FieldEnteredNotCorrect(string error)
        {
            message = error;
        }

        public override string MessageError()
        {
            return message + ". Please try again.";
        }
    }
}

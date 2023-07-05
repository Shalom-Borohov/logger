using System;
using System.Collections.Generic;
using static Logger.Constants.Messages;
using System.Linq;

namespace Logger.IO
{
    public class Outputter
    {
        public void AskTravelerName() => Console.WriteLine(TravelerNameQuestion);
        public void ShowNewLine() => Console.WriteLine();
        public void BlinkRightEye() => Console.WriteLine(BlinkedRightEyeLog);
        public void BlinkLeftEye() => Console.WriteLine(BlinkedLeftEyeLog);
        public void FinishProgramLog() => Console.WriteLine(FinishedProgramLog);
        public void ThankTraveler() => Console.WriteLine(TravelerThankYou);
        public void Show(IEnumerable<string> messages) => messages.ToList().ForEach(Console.WriteLine);
        public void ShowExceptionMessage(Exception exception) => Console.WriteLine(exception.Message);
        public void ShowMenu(string username) => Console.WriteLine(BuildMenu(username));
        public void ShowBadInputError() => Console.WriteLine(BadInputError);
    }
}

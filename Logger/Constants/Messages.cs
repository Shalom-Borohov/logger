using System.Text;
using static Logger.Main.Enums.MenuOptions;

namespace Logger.Constants
{
    public static class Messages
    {
        public const string TravelerNameQuestion = "What is your name traveler?";
        public const string NoLogDefinedError = "Error! No log is defined to be used in the program";
        public const string MenuShowedLog = "Showed menu";
        public const string BlinkedRightEyeLog = "Blinked in right eye!";
        public const string BlinkedLeftEyeLog = "Blinked in left eye!";
        public const string FinishedProgramLog = "Finished program";
        public const string TravelerThankYou = "Thank you traveler";
        public const string OptionPickQuestion = "What would you pick?";
        public const string BadInputError = "Nope my friend, please try again";
        public static readonly string BlinkRightEyeOption = $"{(int)MenuOption.BlinkRightEye}. Blink your right eye";
        public static readonly string BlinkLeftEyeOption = $"{(int)MenuOption.BlinkLeftEye}. Blink your left eye";
        public static readonly string AdministrationOption = $"{(int)MenuOption.Administration}. Administration";
        public static readonly string ExitOption = $"{(int)MenuOption.Exit}. Exit";
        public static string GetUsernameLog(string username) => $"User entered username {username}";
        public static string GetContestWelcome(string username) => $"Welcome {username} to the Great Blinking Contest!";

        public static string BuildMenu(string username)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetContestWelcome(username));
            stringBuilder.AppendLine(OptionPickQuestion);
            stringBuilder.AppendLine(BlinkRightEyeOption);
            stringBuilder.AppendLine(BlinkLeftEyeOption);
            stringBuilder.AppendLine(AdministrationOption);
            stringBuilder.AppendLine(ExitOption);

            return stringBuilder.ToString();
        }
    }
}

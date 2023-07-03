using Logger.src.Exceptions;
using System;
using System.Collections.Generic;
using static Logger.src.main.enums.MenuOptions;
using static Logger.src.loggers.enums.SeverityLevel;
using Logger.src.loggers.classes;
using System.Linq;

namespace Logger.src.main.classes
{
    internal class Menu
    {
        public Dictionary<int, Action> methodsByMenuOption = new Dictionary<int, Action>();
        public List<DateTime> logEntriesDates = new List<DateTime>();
        public loggers.classes.Logger LoggerInstance { get; set; } = null;

        public Menu()
        {
            methodsByMenuOption.Add((int)MenuOption.BlinkRightEye, BlinkRightEye);
            methodsByMenuOption.Add((int)MenuOption.BlinkLeftEye, BlinkLeftEye);
            methodsByMenuOption.Add((int)MenuOption.Administration, ShowLogsUntilNow);
            methodsByMenuOption.Add((int)MenuOption.Exit, Exit);
        }

        public string GetUsernameInput()
        {
            if (LoggerInstance == null) throw new NoLogDefinedException("Error! No log is defined to be used in the program");
            Console.WriteLine("What is your name traveler?");
            string username = Console.ReadLine();
            Console.WriteLine();
            WriteInfoEntry($"User entered username {username}");

            return username;
        }

        public void ShowMenu(string username)
        {
            WriteInfoEntry("Showing menu");
            Console.WriteLine($"Welcome {username} to the Great Blinking Contest! {Environment.NewLine}" +
                $"What would you pick? {Environment.NewLine}" +
                $"{(int)MenuOption.BlinkRightEye}. Blink your right eye {Environment.NewLine}" +
                $"{(int)MenuOption.BlinkLeftEye}. Blink your left eye {Environment.NewLine}" +
                $"{(int)MenuOption.Administration}. Administration {Environment.NewLine}" +
                $"{(int)MenuOption.Exit}. Exit {Environment.NewLine}");
        }

        public void ChooseOption()
        {
            string option = Console.ReadLine();
            methodsByMenuOption[Int32.Parse(option)]();
        }

        private void BlinkRightEye()
        {
            string msg = "Blinked in right eye!";
            WriteInfoEntry(msg);
            Console.WriteLine(msg);
        }

        private void BlinkLeftEye()
        {
            string msg = "Blinked in left eye!";
            WriteInfoEntry(msg);
            Console.WriteLine(msg);
        }

        private void ShowLogsUntilNow() =>
            logEntriesDates.ForEach((DateTime dateTime) => LoggerInstance.ReadEntries(dateTime).ToList().ForEach(Console.WriteLine));

        private void Exit()
        {
            string msg = "Finished program";
            WriteInfoEntry(msg);
            Console.WriteLine($"{Environment.NewLine}Thank you traveler");
            Environment.Exit(0);
        }

        private void WriteInfoEntry(string msg)
        {
            DateTime now = DateTime.Now;
            AddDateIfNotExists(now);
            LoggerInstance.WriteEntry(new LogEntry { DateAndTime = now, Message = msg, Severity = Severity.Information });
        }

        private void AddDateIfNotExists(DateTime now)
        {
            if (!logEntriesDates.Exists((DateTime dateTime) => dateTime.ToString().Equals(now.ToString())))
            {
                logEntriesDates.Add(now);
            }
        }
    }
}

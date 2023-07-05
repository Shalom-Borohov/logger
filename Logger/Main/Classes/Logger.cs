using Logger.Exceptions;
using System;
using System.Collections.Generic;
using static Logger.Main.Enums.MenuOptions;
using Logger.Loggers.Interfaces;
using Logger.IO;
using static Logger.Constants.Messages;
using Logger.Utils;

namespace Logger.Main.Classes
{
    public class Logger
    {
        public Outputter outputter = new Outputter();
        public Inputter inputter = new Inputter();
        public ILog LoggerInstance { get; set; }
        public List<DateTime> LogEntriesDates = new List<DateTime>();
        public Dictionary<int, Action> MethodsByMenuOption = new Dictionary<int, Action>();

        public Logger()
        {
            MethodsByMenuOption.Add((int)MenuOption.BlinkRightEye, BlinkRightEye);
            MethodsByMenuOption.Add((int)MenuOption.BlinkLeftEye, BlinkLeftEye);
            MethodsByMenuOption.Add((int)MenuOption.Administration, ShowLogsUntilNow);
            MethodsByMenuOption.Add((int)MenuOption.Exit, Exit);
        }

        public void Start()
        {
            try
            {
                if (LoggerInstance == null)
                {
                    throw new NoLogDefinedException(NoLogDefinedError);
                }

                outputter.AskTravelerName();
                var username = inputter.ReadLine();
                LogAfterAction(outputter.ShowNewLine, GetUsernameLog(username));
                ShowMenuRepeatedly(username);
            }
            catch (NoLogDefinedException exception)
            {
                LogException(exception);
                outputter.ShowExceptionMessage(exception);
            }
            catch (Exception exception)
            {
                LogException(exception);
                outputter.ShowExceptionMessage(exception);
            }
        }

        private void BlinkRightEye() => LogAfterAction(outputter.BlinkRightEye, BlinkedRightEyeLog);
        private void BlinkLeftEye() => LogAfterAction(outputter.BlinkLeftEye, BlinkedLeftEyeLog);
        private void ShowLogsUntilNow() => LogEntriesDates.ForEach(ShowEntriesByDateTime);
        private void ShowEntriesByDateTime(DateTime dateTime) => outputter.Show(LoggerInstance.ReadEntries(dateTime));

        private void Exit()
        {
            outputter.ShowNewLine();
            LogAfterAction(outputter.ThankTraveler, FinishedProgramLog);
            Environment.Exit(0);
        }

        private void ShowMenuRepeatedly(string username)
        {
            outputter.ShowMenu(username);
            var option = inputter.ReadParsedLine();
            MethodsByMenuOption[option]();

            while (option != (int)MenuOption.Exit)
            {
                try
                {
                    outputter.ShowNewLine();
                    outputter.ShowMenu(username);
                    option = inputter.ReadParsedLine();
                    EnumUtil enumUtil = new EnumUtil();
                    MethodsByMenuOption[option]();
                }
                catch (Exception exception)
                {
                    outputter.ShowBadInputError();
                    LogException(exception);
                }
            };
        }

        private void LogAfterAction(Action doSomething, string message)
        {
            DateTime now = DateTime.Now;
            LoggerInstance.WriteInfoEntry(message, now);
            AddNonExistentDate(now);
            doSomething();
        }

        private void LogException(Exception exception)
        {
            DateTime now = DateTime.Now;
            LoggerInstance.WriteErrorEntry(exception.Message, now);
            AddNonExistentDate(now);
        }

        private void AddNonExistentDate(DateTime now)
        {
            if (!LogEntriesDates.Exists((DateTime dateTime) => dateTime.ToString().Equals(now.ToString())))
            {
                LogEntriesDates.Add(now);
            }
        }
    }
}

using Logger.src.Exceptions;
using System;
using Logger.src.loggers.classes;
using Logger.src.loggers.interfaces;

namespace Logger.src.main.classes
{
    internal class Driver
    {
        public void Start()
        {
            try
            {
                Menu menu = InitMenu();
                string username = menu.GetUsernameInput();
                ShowMenuRepeatedly(menu, username);
            }
            catch (NoLogDefinedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Menu InitMenu()
        {
            ILog eventLog = new EncryptedCsvFileLog { FilePath = "encryptedLogs.csv" };

            return new Menu() { LoggerInstance = new loggers.classes.Logger { LoggerInstance = eventLog } };
        }

        private void ShowMenuRepeatedly(Menu menu, string username)
        {
            while (true)
            {
                menu.ShowMenu(username);
                menu.ChooseOption();
                Console.WriteLine();
            };
        }
    }
}

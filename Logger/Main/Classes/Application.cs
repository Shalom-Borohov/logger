using Logger.Loggers.Classes;
using Logger.Loggers.Interfaces;
using Logger.Utils;
using System;
using System.IO;
using static Logger.Loggers.Enums.LoggerTypes;

namespace Logger.Main.Classes
{
    public class Application
    {
        public void Start()
        {
            LoadDotEnv();
            var loggerCreator = new LoggerCreator();
            ILog loggerInstance = loggerCreator.Create(GetEnvLoggerType());
            var logger = new Logger { LoggerInstance = loggerInstance };
            logger.Start();
        }

        private void LoadDotEnv()
        {
            var root = Directory.GetCurrentDirectory();
            var dotenvPath = Path.Combine(root, ".env");
            var dotEnv = new DotEnv();
            dotEnv.Load(dotenvPath);
        }

        private LoggerType GetEnvLoggerType()
        {
            var enumUtil = new EnumUtil();
            return enumUtil.ParseEnum<LoggerType>(Environment.GetEnvironmentVariable("LOGGER_TYPE"));
        }

    }
}

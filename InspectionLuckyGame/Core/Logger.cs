using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using Microsoft.Xaml.Behaviors.Layout;

namespace InspectionLuckyGame.Core
{
    public class Logger
    {
        private readonly ILog log = LogManager.GetLogger(typeof(Logger));

        private Hierarchy hierarchy;

        public Logger()
        {
            log = log4net.LogManager.GetLogger("Log");
            hierarchy = log4net.LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
            hierarchy.Configured = true;

            AddApender(log);
        }

        public void Write(string message)
        {
            this.log.Debug(message);
        }

        private void AddApender(log4net.ILog log)
        {
            log4net.Repository.Hierarchy.Logger logger = (log4net.Repository.Hierarchy.Logger)log.Logger;
            log4net.Appender.RollingFileAppender appender = new log4net.Appender.RollingFileAppender();

            appender.File = System.IO.Path.Combine("Log", logger.Name + ".log");


            appender.PreserveLogFileNameExtension = true;
            appender.StaticLogFileName = false;
            appender.Encoding = System.Text.Encoding.Unicode;
            appender.AppendToFile = true;
            appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;

            appender.MaxSizeRollBackups = 0;
            appender.MaximumFileSize = "5MB";

            appender.DatePattern = "yyyyMMdd";
            appender.Layout = new log4net.Layout.PatternLayout("[%date{yyyy-MM-dd} %date{HH:mm:ss.fff}] %message%newline");

            appender.ActivateOptions();

            logger.AddAppender(appender);
            logger.Hierarchy = hierarchy;
            logger.Level = logger.Hierarchy.LevelMap["ALL"];

            log4net.Appender.ConsoleAppender appenderConsole = new log4net.Appender.ConsoleAppender();
            appenderConsole.Layout = new log4net.Layout.PatternLayout("[%date{yyyy-MM-dd} %date{HH:mm:ss.fff}] %message%newline");
            appenderConsole.ActivateOptions();
            logger.AddAppender(appenderConsole);
        }
    }
}

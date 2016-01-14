using NLog;
using System;

namespace Logging
{
    public class Logger
    {
        /// <summary>
        /// NLog Logger for the calling project/class
        /// </summary>
        private readonly NLog.Logger _nLogger;

        /// <summary>
        /// Creates the logger for the calling project/class
        /// </summary>
        /// <param name="loggingType">Enum for the class</param>
        public Logger(LoggingType loggingType)
        {
            _nLogger = LogManager.GetLogger(loggingType.ToString());
        }

        /// <summary>
        /// Logs an info message
        /// </summary>
        /// <param name="message">The message</param>
        public void Info(string message)
        {
            _nLogger.Info(message);
        }

        /// <summary>
        /// Logs a warn message
        /// </summary>
        /// <param name="message">The message</param>
        public void Warn(string message)
        {
            var log = new LogEventInfo
            {
                Message = message,
                Level = LogLevel.Warn
            };
            LogEventInfos(log);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="ex">The exception</param>
        public void Error(string message, Exception ex)
        {
            var log = new LogEventInfo
            {
                Message = message,
                Level = LogLevel.Error,
                Exception = ex
            };
            LogEventInfos(log);
        }

        /// <summary>
        /// Logs the LogEventInfo to the specified "location"
        /// </summary>
        /// <param name="log"></param>
        private void LogEventInfos(LogEventInfo log)
        {
            _nLogger.Log(log);
        }
    }
}
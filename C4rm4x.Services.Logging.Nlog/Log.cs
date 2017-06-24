#region Using

using C4rm4x.Services.Framework.Logging;
using NLog;
using System;
using NLogger = NLog.Logger;

#endregion

namespace C4rm4x.Services.Logging.Nlog
{
    /// <summary>
    /// Implementation of ILog using Nlog
    /// </summary>
    public class Log : ILog
    {
        private static NLogger CurrentLogger =>
            new Lazy<NLogger>(() => LogManager.GetCurrentClassLogger()).Value;

        /// <summary>
        /// Logs a message as Debug
        /// </summary>
        /// <param name="message">The message</param>
        public void Debug(string message)
        {
            CurrentLogger.Debug(message);
        }

        /// <summary>
        /// Logs a message as Error
        /// </summary>
        /// <param name="message">The message</param>
        public void Error(string message)
        {
            CurrentLogger.Error(message);
        }

        /// <summary>
        /// Logs a message as Error and adds exception trace
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="exceptionThrown">The exception to be traced</param>
        public void Error(string message, Exception exceptionThrown)
        {
            CurrentLogger.Error(exceptionThrown, message);
        }

        /// <summary>
        /// Logs a message as Info
        /// </summary>
        /// <param name="message">The message</param>
        public void Info(string message)
        {
            CurrentLogger.Info(message);
        }

        /// <summary>
        /// Logs a message as Warning
        /// </summary>
        /// <param name="message">The message</param>
        public void Warning(string message)
        {
            CurrentLogger.Warn(message);
        }
    }
}

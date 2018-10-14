using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using System;

namespace DotNetPerformance.Shared.Extensions.Logging
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger, LogLevel logLevel, Exception ex, params object[] args)
        {
            var format = new Func<object, Exception, string>(MessageFormatter);
            logger.Log(logLevel, 0, new FormattedLogValues(ex.ToString(), args), null, format);
        }

        private static string MessageFormatter(object state, Exception error) => state.ToString();
    }
}

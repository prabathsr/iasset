using System;
using NLog;

namespace iasset.Weather.Logger
{
    /// <summary>
    /// Currently, logs are written into "%appdata%\iasset Weather Viewer" folder. Check NLog.config for more details
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiLogger<T> : IApiLogger<T>
        where T : class
    {
        private readonly NLog.Logger _log;

        public ApiLogger()
        {
            _log = LogManager.GetLogger(typeof(T).FullName);
        }

        public void Error(string message, Exception exp)
        {
            Log(LogLevel.Error, message, exp);
        }

        private void Log(LogLevel level, string message, Exception exp)
        {
            _log.Log(level, exp, message);
        }
    }
}
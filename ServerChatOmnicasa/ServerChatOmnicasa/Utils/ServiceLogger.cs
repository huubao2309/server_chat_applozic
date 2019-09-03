using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace ServerChatOmnicasa.Utils
{

    public class ServiceLogger
    {
        #region Others

        private enum Category
        {
            Info,
            Error
        }

        #endregion

        #region Fields

        private static string _prefixLogName = "log";
        private static string _logFolderPath { get; set; }

        private static Serilog.Core.Logger _loggerInformation;
        private static Serilog.Core.Logger _loggerError;

        #endregion

        #region Contructors

        public ServiceLogger(string logFolderPath)
        {
            if (!Directory.Exists(logFolderPath))
                Directory.CreateDirectory(logFolderPath);

            _logFolderPath = logFolderPath;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Log message
        /// </summary>
        public void Info(string message,
            [CallerMemberName] string sourceMemberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNo = 0)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    LogInformation(CreateLogStringData(message, sourceMemberName, sourceFilePath, sourceLineNo));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });
        }

        /// <summary>
        /// Log exception message
        /// </summary>
        public void Exception(Exception exception,
            [CallerMemberName] string sourceMemberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNo = 0)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    LogException(CreateExceptionLogStringData(exception, sourceMemberName, sourceFilePath, sourceLineNo));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });
        }

        #endregion

        #region Private Methods

        private static void LogInformation(string message)
        {
            // Create logger
            if (_loggerInformation == null)
            {
                var loggerConfig = CreateLoggerConfiguration(Category.Info, _logFolderPath, $"{_prefixLogName}_.txt");
                _loggerInformation = loggerConfig.CreateLogger();
            }

            // Log
            _loggerInformation.Information(message);
        }

        private static void LogException(string message)
        {
            // Create logger
            if (_loggerError == null)
            {
                var loggerConfig = CreateLoggerConfiguration(Category.Error, _logFolderPath, $"ex_{_prefixLogName}_.txt");
                _loggerError = loggerConfig.CreateLogger();
            }

            // Log
            _loggerError.Error(message);
        }

        private static LoggerConfiguration CreateLoggerConfiguration(Category category, string folderPath, string fileName)
        {
            LogEventLevel logEventLevel;
            switch (category)
            {
                case Category.Error:
                    logEventLevel = LogEventLevel.Error;
                    break;

                default:
                    logEventLevel = LogEventLevel.Information;
                    break;
            }

            return new LoggerConfiguration().WriteTo.File(
                Path.Combine(folderPath, fileName)
                , logEventLevel
                , "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Message:lj}{NewLine}{Exception}"
                , rollOnFileSizeLimit: true
                , fileSizeLimitBytes: 10 * 1024 * 1024
                , retainedFileCountLimit: null
                , rollingInterval: RollingInterval.Hour);
        }

        private static string CreateLogStringData(string message, string sourceMemberName, string sourceFilePath, int sourceLineNo)
        {
            try
            {
                // Create result
                var result = JsonConvert.SerializeObject(new
                {
                    data = message,
                    info = $"Method {sourceMemberName} line {sourceLineNo} in {ConvertSourceFilePath(sourceFilePath)}"
                }, Formatting.Indented);

                // Result
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return message;
            }
        }

        private static string CreateExceptionLogStringData(Exception exception, string sourceMemberName, string sourceFilePath, int sourceLineNo)
        {
            try
            {
                // Create result
                var result = JsonConvert.SerializeObject(new
                {
                    exception = $"{exception?.GetType()?.Name}: {exception}",
                    trace = $"{exception?.GetType()?.Name}: {exception?.StackTrace}",
                    info = $"Method {sourceMemberName} line {sourceLineNo} in {ConvertSourceFilePath(sourceFilePath)}"
                }, Formatting.Indented);

                // Result
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return exception?.ToString();
            }
        }

        private static string ConvertSourceFilePath(string sourceFilePath)
        {
            var parts = sourceFilePath.Split('\\');
            if (!parts.Any() || parts.Length < 3)
            {
                parts = sourceFilePath.Split('/');
                if (!parts.Any() || parts.Length < 3)
                    return sourceFilePath;
            }

            // Get names
            var rootFolderName = parts[parts.Length - 3];
            var lastDirectoryName = parts[parts.Length - 2];
            var fileName = parts[parts.Length - 1];

            // Result
            return $"{rootFolderName}\\{lastDirectoryName}\\{fileName}";
        }

        #endregion

    }
}

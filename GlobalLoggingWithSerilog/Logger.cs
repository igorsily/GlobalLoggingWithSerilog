using Serilog;
using System.Data.SqlClient;

namespace GlobalLoggingWithSerilog
{
    public class Logger
    {
        private static readonly Serilog.ILogger _errorLogger;

        static Logger()
        {
            _errorLogger = new LoggerConfiguration().WriteTo.File(path: "")
                .CreateLogger();
        }

        public static void WriteError(LogDetail infoToLog)
        {

            if (infoToLog.Exception != null)
            {
                var procName = FindProcName(infoToLog.Exception);
                infoToLog.Location = String.IsNullOrEmpty(procName) ? infoToLog.Location : procName;
                infoToLog.Message = GetMessageFromException(infoToLog.Exception);
            }

            _errorLogger.Write(Serilog.Events.LogEventLevel.Information, "{@LogDetail}", infoToLog);
        }

        private static string GetMessageFromException(Exception ex)
        {
            if (ex.InnerException is not null)
            {
                return GetMessageFromException(ex.InnerException);
            }

            return ex.Message;
        }

        private static string FindProcName(Exception ex)
        {
            var sqlEx = ex as SqlException;

            if (sqlEx != null)
            {
                var procName = sqlEx.Procedure;

                if (!string.IsNullOrEmpty(procName))
                {
                    return procName;
                }
            }

            if (!string.IsNullOrEmpty((string)ex.Data["Procedure"])){
                return (string)ex.Data["Procedure"];
            }

            if(ex.InnerException != null)
            {
                return FindProcName(ex.InnerException);
            }

            return null;
        }
    }
}

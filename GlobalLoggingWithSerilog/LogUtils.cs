namespace GlobalLoggingWithSerilog
{
    public class LogUtils
    {
        private static LogDetail GetLogDetail(string message, Exception ex)
        {
            return new LogDetail
            {
                Product = "Logger",
                Location = "LoggerConsole",
                Layer = "Job",
                User = Environment.UserName,
                HostName = Environment.MachineName,
                Message = message,
                Exception = ex
            };
        }
    }
}

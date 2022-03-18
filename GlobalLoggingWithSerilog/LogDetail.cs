namespace GlobalLoggingWithSerilog
{
    public class LogDetail
    {

        public LogDetail()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }

        public string Message { get;  set; }

        public string Product { get; set; }

        public string Layer { get; set; }

        public string Location { get; set; }

        public string HostName { get; set; }

        public string User { get; set; }

        public long? ElapseMilliseconds { get; set; } //Only for performance entries

        public Exception Exception { get; set; } // The exception for error loggin

        public string CorrelationId { get; set; } //exception shielding from server to client

        public Dictionary<string, object> AdditionalInfo  { get; set; }
    }

 
}

using System.Diagnostics;
using System.Globalization;

namespace GlobalLoggingWithSerilog
{
    public class Tracker
    {

        private readonly Stopwatch _sw;

        private readonly LogDetail _logDetail;


        public Tracker(string message, string user, string location, string product, string layer)
        {

            _sw = Stopwatch.StartNew();

            _logDetail = new LogDetail()
            {
                Message = message,
                User = user,
                Product = product,
                Layer = layer,
                Location = location,
                HostName = Environment.MachineName,
            };

            var beginTime = DateTime.Now;
            _logDetail.AdditionalInfo = new Dictionary<string, object>()
            {
                {"Started", beginTime.ToString(CultureInfo.InvariantCulture) }
            };
        }
        public Tracker(string message, string user, string location, 
            string product, string layer, Dictionary<string, object> valuePairs) : this(message, user
               , location, product, layer)
        {

           foreach (var pair in valuePairs)
            {
                _logDetail.AdditionalInfo.Add("input-" + pair.Key, pair.Value);
            }
        }

        public void Stop()
        {
            _sw.Stop();
            _logDetail.ElapseMilliseconds = _sw.ElapsedMilliseconds;
            //Logger.WriteError(_logDetail);  
        }
    }
}

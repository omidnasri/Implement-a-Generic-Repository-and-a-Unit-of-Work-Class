using System;
using System.Web;

namespace Karin.BaseConfig.Security.Throttle
{
    [Serializable]
    public class ThrottleLogEntry
    {
        public string RequestId { get; set; }
        public string ClientIp { get; set; }
        public string ClientKey { get; set; }
        public string Endpoint { get; set; }
        public long TotalRequests { get; set; }
        public System.DateTime StartPeriod { get; set; }
        public long RateLimit { get; set; }
        public string RateLimitPeriod { get; set; }
        public System.DateTime LogDate { get; set; }
        public HttpRequestBase Request { get; set; }
    }
}

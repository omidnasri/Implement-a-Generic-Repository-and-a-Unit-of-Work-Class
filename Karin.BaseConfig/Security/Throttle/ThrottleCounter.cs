using System;

namespace Karin.BaseConfig.Security.Throttle
{
    /// <summary>
    /// Stores the initial access time and the numbers of calls made from that point
    /// </summary>
    [Serializable]
    public struct ThrottleCounter
    {
        public System.DateTime Timestamp { get; set; }
        public long TotalRequests { get; set; }
    }
}

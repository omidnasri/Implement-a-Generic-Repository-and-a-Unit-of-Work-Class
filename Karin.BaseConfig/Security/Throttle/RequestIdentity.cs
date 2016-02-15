﻿using System;

namespace Karin.BaseConfig.Security.Throttle
{
    /// <summary>
    /// Stores the client ip, key and endpoint
    /// </summary>
    [Serializable]
    public class RequestIdentity
    {
        public string ClientIp { get; set; }
        public string ClientKey { get; set; }
        public string Endpoint { get; set; }

        public override string ToString()
        {
            return string.Format("{0}_{1}_{2}", ClientIp, ClientKey, Endpoint);
        }
    }
}

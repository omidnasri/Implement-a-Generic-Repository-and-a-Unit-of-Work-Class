using System.Net;
using System.Text.RegularExpressions;

namespace Karin.BaseConfig.Net
{
    public partial class Net
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string IpAddress()
        {
            try
            {
                string externalIp = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIp = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIp)[0].ToString();
                return externalIp;
            }
            catch { return null; }
        }
    }
}

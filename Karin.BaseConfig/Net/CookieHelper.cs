using System;
using System.Web;

namespace Karin.BaseConfig.Net
{
    public class CookieHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        public static void AddCookie(string name, string value, System.DateTime expireTime)
        {
            var newCookie = new HttpCookie(name) { Value = value, Expires = expireTime };
            HttpContext.Current.Response.SetCookie(newCookie);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookieValue(string name)
        {
            HttpCookie myCookie = new HttpCookie("_languageId");
            myCookie = HttpContext.Current.Request.Cookies["_languageId"];

            return HttpContext.Current.Request.Cookies[name].Value;
        }

        public static int GetCourrentLanguageId()
        {
            var httpCookie = HttpContext.Current.Request.Cookies["_languageId"];
            if (httpCookie != null)
                return int.Parse(httpCookie.Value);
            throw new NullReferenceException("Return GetLanguageId is null");
        }

        public static string CurrentAbbrevation()
        {
            return GetCookieValue("_culture");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void UpdateCookie(string name, string value)
        {
            HttpCookie userLanguageIdCookie = HttpContext.Current.Request.Cookies[name];
            userLanguageIdCookie.Value = value;
            HttpContext.Current.Response.SetCookie(userLanguageIdCookie);
        }
    }
}

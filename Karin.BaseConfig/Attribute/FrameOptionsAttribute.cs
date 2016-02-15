using System;
using System.Web;
using System.Web.Mvc;

namespace Karin.BaseConfig.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class FrameOptionsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Indicate the scenarios in which the response may be hosted in an iframe. See https://developer.mozilla.org/en-US/docs/HTTP/X-Frame-Options for more information.
        /// </summary>
        public enum FrameOptions
        {
            /// <summary>
            /// Response is never allowed in an iframe.
            /// </summary>
            Deny,
            // Response is only allowed in an iframe if the hosting page is from the same origin.
            SameOrigin,
            // Response is only allowed in an iframe is the hosting page is from the specified origin.
            CustomOrigin
        }
        /// <summary>
        /// 
        /// </summary>
        public const string HeaderName = "X-Frame-Options";
        /// <summary>
        /// 
        /// </summary>
        private const string Deny = "DENY";
        /// <summary>
        /// 
        /// </summary>
        public const string SameOrigin = "SAMEORIGIN";
        /// <summary>
        /// 
        /// </summary>
        public const string AllowFrom = "ALLOW-FROM {0}";
        /// <summary>
        /// 
        /// </summary>
        public readonly string CustomOrigin;
        private readonly FrameOptions _options;
        /// <summary>
        /// This default ctor uses FrameOptions.Deny.
        /// </summary>
        public FrameOptionsAttribute()
        {
            _options = FrameOptions.Deny;
        }
        /// <summary>
        /// Use this ctor to specify the FrameOptions.
        /// </summary>
        /// <param name="options">FrameOptions to use.</param>
        public FrameOptionsAttribute(FrameOptions options)
        {
            _options = options;
        }
        /// <summary>
        /// Use this ctor to specify a custom origin.
        /// </summary>
        /// <param name="origin">The origin to allow.</param>
        public FrameOptionsAttribute(string origin)
        {
            if (string.IsNullOrWhiteSpace(origin)) throw new ArgumentNullException(nameof(origin));

            _options = FrameOptions.CustomOrigin;
            CustomOrigin = origin;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException(nameof(filterContext));

            if (filterContext.IsChildAction) return;

            string value;
            switch (_options)
            {
                case FrameOptions.Deny:
                    value = Deny;
                    break;
                case FrameOptions.SameOrigin:
                    value = SameOrigin;
                    break;
                case FrameOptions.CustomOrigin:
                    value = GetCustomOriginHeaderValue(filterContext.HttpContext.Request);
                    break;
                default:
                    throw new Exception("Invalid FrameOptions");
            }

            filterContext.HttpContext.Response.AddHeader(HeaderName, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetCustomOriginHeaderValue(HttpRequestBase request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var origin = GetCustomOrigin(request);
            return string.IsNullOrWhiteSpace(origin) ? Deny : string.Format(AllowFrom, origin);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual string GetCustomOrigin(HttpRequestBase request)
        {
            return CustomOrigin;
        }
    }
}

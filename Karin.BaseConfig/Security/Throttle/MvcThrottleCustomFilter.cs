using System.Web.Mvc;
using System.Web.Routing;

namespace Karin.BaseConfig.Security.Throttle
{
    public class MvcThrottleCustomFilter : ThrottlingFilter
    {
        protected override ActionResult QuotaExceededResult(RequestContext filterContext, string message, System.Net.HttpStatusCode responseCode, string requestId)
        {
            var rateLimitedView = new ViewResult
            {
                ViewName = "Error",
                ViewData = {["Message"] = message,Model = new Error()
                {
                    Title = "Bad_Request",
                    DateTime = System.DateTime.Now,
                    Body ="This_Account_Has_Been_Locked_Out_Please_Try_Again_Later" ,
                    Description = ""
                } }
            };
            return rateLimitedView;
        }
    }

    public class Error
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public System.DateTime DateTime { get; set; }
    }
}
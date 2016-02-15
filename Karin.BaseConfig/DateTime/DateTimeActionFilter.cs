using System.Web.Mvc;

namespace Karin.BaseConfig.DateTime
{
    public class DateTimeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //if (System.Threading.Thread.CurrentThread.CurrentCulture.LCID == 1065)
            if (System.Threading.Thread.CurrentThread.CurrentCulture.EnglishName.ToLower() == "persian")
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = new PersianCulture();
        }
    }
}

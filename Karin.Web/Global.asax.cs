using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Karin.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Strat Project
            //if (!Database.Exists("DefaultConnection")) new Seed();
            //
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Engines
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            //
            //ControllerBuilder.Current.SetControllerFactory(typeof(DecryptingControllerFactory));
        }
    }
}

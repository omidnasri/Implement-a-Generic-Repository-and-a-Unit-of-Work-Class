using System;
using System.Linq;
using System.Web.Mvc;
using Karin.BaseConfig.MvcHtmlHelper;

namespace Karin.BaseConfig.Security.ControllerFactory
{
    /// <summary>
    /// 
    /// </summary>
    public class DecryptingControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEncryptSettingsProvider _settings;
        /// <summary>
        /// 
        /// </summary>
        public DecryptingControllerFactory()
        {
            _settings = new EncryptSettingsProvider();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            //
            if (controllerName.StartsWith("k_")) { controllerName = controllerName.Replace("k_", string.Empty); controllerName = global::Karin.BaseConfig.Security.Encryption.StringEncoder.Decode(controllerName); }
            if (requestContext.RouteData.Values.Any(z => z.Value.ToString().StartsWith("a_")))
            {
                var value = requestContext.RouteData.Values["action"];
                requestContext.RouteData.Values.Remove("action");
                var t = value.ToString().Replace("a_", string.Empty);
                requestContext.RouteData.Values.Add("action", global::Karin.BaseConfig.Security.Encryption.StringEncoder.Decode(t));

                var value2 = requestContext.RouteData.Values["controller"];
                requestContext.RouteData.Values.Remove("controller");
                var t2 = value2.ToString().Replace("k_", string.Empty);
                requestContext.RouteData.Values.Add("controller", global::Karin.BaseConfig.Security.Encryption.StringEncoder.Decode(t2));
            }
            var request = requestContext.HttpContext.Request;
            var parameters = request.Params;
            var encryptedParamKeys = parameters.AllKeys.Where(x => x.StartsWith("v_")).ToList();
            //
            foreach (var key in encryptedParamKeys)
            {
                var oldKey = (key).Replace("v_", string.Empty);
                oldKey = global::Karin.BaseConfig.Security.Encryption.StringEncoder.Decode(oldKey);
                var oldValue = global::Karin.BaseConfig.Security.Encryption.StringEncoder.Decode(parameters[key]);
                if (requestContext.RouteData.Values[oldKey] != null)
                {
                    if (requestContext.RouteData.Values[oldKey].ToString() != oldValue)
                        throw new ApplicationException("Form values is modified!");
                }
                requestContext.RouteData.Values[oldKey] = oldValue;
            }
            //
            return base.CreateController(requestContext, controllerName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        private IRijndaelStringEncrypter GetDecrypter(System.Web.Routing.RequestContext requestContext)
        {
            var decrypter = new RijndaelStringEncrypter(_settings, requestContext.GetActionKey());
            return decrypter;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Karin.BaseConfig.MvcHtmlHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionKey
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ActionKeyValue { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ActionKeyService : IActionKeyService
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly IList<ActionKey> ActionKeys;
        /// <summary>
        /// 
        /// </summary>
        static ActionKeyService()
        {
            ActionKeys = new List<ActionKey>
            {
                new ActionKey {
                    Area = "",
                    Controller = "Account",
                    Action = "FormGenerator",
                    ActionKeyValue = "078bd2c7-ad1b-40f9-9733-b1b32c136010",
                },
            };
        }
        /// <summary>
        /// پیدا کردن کلید متناظر هر ویو.ایجاد کلید جدید در صورت عدم وجود کلید در سیستم
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public string GetActionKey(string action, string controller, string area = "")
        {
            // 
            area = area ?? "";
            // 
            var actionKey = ActionKeys.FirstOrDefault(a => string.Equals(a.Action, action, StringComparison.CurrentCultureIgnoreCase)
                                                        && string.Equals(a.Controller, controller, StringComparison.CurrentCultureIgnoreCase)
                                                        && string.Equals(a.Area, area, StringComparison.CurrentCultureIgnoreCase));
            // 
            return actionKey != null ? actionKey.ActionKeyValue
                                     : AddActionKey(action, controller, area);
        }
        /// <summary>
        /// اضافه کردن کلید جدید به سیستم
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private static string AddActionKey(string action, string controller, string area = "")
        {
            var actionKey = new ActionKey
            {
                Action = action,
                Controller = controller,
                Area = area,
                ActionKeyValue = Guid.NewGuid().ToString()
            };
            ActionKeys.Add(actionKey);
            return actionKey.ActionKeyValue;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class Encrypted
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public static string GetActionKey(this System.Web.Routing.RequestContext requestContext)
        {
            IActionKeyService actionKeyService = new ActionKeyService();
            var action = requestContext.RouteData.Values["Action"].ToString();
            var controller = requestContext.RouteData.Values["Controller"].ToString();
            var area = requestContext.RouteData.Values["Area"];
            var actionKeyValue = actionKeyService.GetActionKey(action, controller, area?.ToString());

            return actionKeyValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string GetActionKey(this HtmlHelper helper)
        {
            IActionKeyService actionKeyService = new ActionKeyService();
            var action = helper.ViewContext.RouteData.Values["Action"].ToString();
            var controller = helper.ViewContext.RouteData.Values["Controller"].ToString();
            var area = helper.ViewContext.RouteData.Values["Area"];
            var actionKeyValue = actionKeyService.GetActionKey(action, controller, area?.ToString());
            return actionKeyValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString EncryptedHidden(this HtmlHelper helper, string name, object value)
        {
            if (value == null) { value = String.Empty; }
            var strValue = value.ToString();
            var strName = name;
            var encryptedValue = global::Karin.BaseConfig.Security.Encryption.StringEncoder.Encode(strValue);
            var encryptedName = global::Karin.BaseConfig.Security.Encryption.StringEncoder.Encode(strName);
            var encodedValue = helper.Encode(encryptedValue);
            var newName = String.Concat("v_", encryptedName);
            return helper.Hidden(newName, encodedValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="encryptedType"></param>
        /// <returns></returns>
        public static MvcHtmlString EncryptedHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, EncryptedType encryptedType = EncryptedType.Both)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return EncryptedHidden(htmlHelper, name, metadata.Model);
        }
        /// <summary>
        /// Generates a fully qualified URL to an action method by using
        /// the specified action name, controller name and route values.
        /// </summary>
        public static string EncryptedAction(this UrlHelper url, Task<ActionResult> actionTask, object routeValues = null, PathType pathType = PathType.Relative)
        {
            var result = actionTask.Result.GetT4MVCResult();
            // Add routeValues to result.RouteValueDictionary.Add(...) 

            return url.Action(actionName: "a_" + global::Karin.BaseConfig.Security.Encryption.StringEncoder.Encode(result.Action), controllerName: "k_" + global::Karin.BaseConfig.Security.Encryption.StringEncoder.Encode(result.Controller), routeValues: result.RouteValueDictionary, protocol: result.Protocol);
        }
        /// <summary>
        /// Generates a fully qualified URL to an action method by using
        /// the specified action name, controller name and route values.
        /// </summary>
        public static string EncryptedAction(this UrlHelper url, ActionResult actionTask, object routeValues = null, PathType pathType = PathType.Relative)
        {
            var result = actionTask.GetT4MVCResult();
            // Add routeValues to result.RouteValueDictionary.Add(...) 

            return url.Action(actionName: "a_" + global::Karin.BaseConfig.Security.Encryption.StringEncoder.Encode(result.Action), controllerName: "k_" + global::Karin.BaseConfig.Security.Encryption.StringEncoder.Encode(result.Controller),
                routeValues: result.RouteValueDictionary, protocol: result.Protocol);

        }
        /// <summary>
        /// Generates a fully qualified URL to an action method by using
        /// the specified action name, controller name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="pathType"></param>
        /// <returns>The absolute URL.</returns>
        public static string EncryptedAction(this UrlHelper url, string actionName, string controllerName, object routeValues = null, PathType pathType = PathType.Relative)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
            return url.Action(actionName, controllerName, routeValues);
        }
        /// <summary>
        /// 
        /// </summary>
        public enum EncryptedType
        {
            Name,
            Value,
            Both
        }
        /// <summary>
        /// 
        /// </summary>
        public enum PathType
        {
            Absolute,
            Relative,
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IActionKeyService
    {
        /// <summary>
        /// پیدا کردن کلید متناظر هر ویو.ایجاد کلید جدید در صورت عدم وجود کلید در سیستم
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        string GetActionKey(string action, string controller, string area = "");
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IEncryptSettingsProvider
    {
        byte[] EncryptionKey { get; }
        string EncryptionPrefix { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class EncryptSettingsProvider : IEncryptSettingsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _encryptionPrefix;
        /// <summary>
        /// 
        /// </summary>
        private readonly byte[] _encryptionKey;
        /// <summary>
        /// 
        /// </summary>
        public EncryptSettingsProvider()
        {
            //read settings from configuration
            var useHashingString = ConfigurationManager.AppSettings["UseHashingForEncryption"];
            var useHashing = String.Compare(useHashingString, "false", System.StringComparison.OrdinalIgnoreCase) != 0;
            _encryptionPrefix = ConfigurationManager.AppSettings["EncryptionPrefix"];
            if (String.IsNullOrWhiteSpace(_encryptionPrefix)) { _encryptionPrefix = "encryptedHidden_"; }
            var key = ConfigurationManager.AppSettings["EncryptionKey"];
            if (useHashing)
            {
                var hash = new SHA256Managed();
                _encryptionKey = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash.Clear();
                hash.Dispose();
            }
            else { _encryptionKey = Encoding.UTF8.GetBytes(key); }
        }
        #region ISettingsProvider Members
        /// <summary>
        /// 
        /// </summary>
        public byte[] EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EncryptionPrefix
        {
            get { return _encryptionPrefix; }
        }
        #endregion

    }
    /// <summary>
    /// 
    /// </summary>
    public interface IRijndaelStringEncrypter : IDisposable
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
    /// <summary>
    /// 
    /// </summary>
    public class RijndaelStringEncrypter : IRijndaelStringEncrypter
    {
        #region Encrypter
        private RijndaelManaged _encryptionProvider;
        private ICryptoTransform _cryptoTransform;
        private readonly byte[] _key;
        private readonly byte[] _iv;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="key"></param>
        public RijndaelStringEncrypter(IEncryptSettingsProvider settings, string key)
        {
            _encryptionProvider = new RijndaelManaged();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var derivedbytes = new Rfc2898DeriveBytes(settings.EncryptionKey, keyBytes, 3);
            _key = derivedbytes.GetBytes(_encryptionProvider.KeySize / 8);
            _iv = derivedbytes.GetBytes(_encryptionProvider.BlockSize / 8);
        }
        #endregion

        #region IEncryptString Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            if (_cryptoTransform == null) { _cryptoTransform = _encryptionProvider.CreateEncryptor(_key, _iv); }
            var encryptedBytes = _cryptoTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            var encrypted = Convert.ToBase64String(encryptedBytes);
            return encrypted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(string value)
        {
            var valueBytes = Convert.FromBase64String(value);
            if (_cryptoTransform == null) { _cryptoTransform = _encryptionProvider.CreateDecryptor(_key, _iv); }
            var decryptedBytes = _cryptoTransform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            var decrypted = Encoding.UTF8.GetString(decryptedBytes);
            return decrypted;
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_cryptoTransform != null)
            {
                _cryptoTransform.Dispose();
                _cryptoTransform = null;
            }

            if (_encryptionProvider == null) return;
            _encryptionProvider.Clear();
            _encryptionProvider.Dispose();
            _encryptionProvider = null;
        }
        #endregion
    }
}

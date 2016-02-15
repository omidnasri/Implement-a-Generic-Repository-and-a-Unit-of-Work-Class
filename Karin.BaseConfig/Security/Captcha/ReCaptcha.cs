using System.Configuration;
using System.Net;
using BaseConfig.Security.Captcha;
using Newtonsoft.Json;

namespace Karin.BaseConfig.Security.Captcha
{
    public class ReCaptcha
    {
        public static string _secret;

        static ReCaptcha()
        {
            _secret = ConfigurationManager.AppSettings["ReCaptchaGoogleSecretKey"];
        }

        public static bool IsValid(string response)
        {
            //secret that was generated in key value pair
            var client = new WebClient();
            var reply = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={_secret}&response={response}");

            var captchaResponse = JsonConvert.DeserializeObject<RepaptchaResponse>(reply);

            // when response is false check for the error message
            if (!captchaResponse.Success)
            {
                //if (captchaResponse.ErrorCodes.Count <= 0) return View();

                //var error = captchaResponse.ErrorCodes[0].ToLower();
                //switch (error)
                //{
                //    case ("missing-input-secret"):
                //        ViewBag.Message = "The secret parameter is missing.";
                //        break;
                //    case ("invalid-input-secret"):
                //        ViewBag.Message = "The secret parameter is invalid or malformed.";
                //        break;

                //    case ("missing-input-response"):
                //        ViewBag.Message = "The response parameter is missing.";
                //        break;
                //    case ("invalid-input-response"):
                //        ViewBag.Message = "The response parameter is invalid or malformed.";
                //        break;

                //    default:
                //        ViewBag.Message = "Error occured. Please try again";
                //        break;
                //}
                return false;
            }
            // Captcha is valid
            return true;
        }
    }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace BaseConfig.Security.Captcha
{
    public class RepaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}

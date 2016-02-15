using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Karin.BaseConfig.Identity2
{
    public class KPasswordValidation : IIdentityValidator<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public int LengthRequired { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        public KPasswordValidation(int length = 6)
        {
            LengthRequired = length;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (String.IsNullOrEmpty(item) || item.Length < LengthRequired)
            {
                return Task.FromResult(IdentityResult.Failed($"{"Max_Lenght_Error_Message"}:{LengthRequired}"));
            }
            var PasswordPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$"; //@"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";
            return Task.FromResult(!Regex.IsMatch(item, PasswordPattern) ? IdentityResult.Failed("The_Password_Must_Have_At_Least_One_Numeric_And_One_SpecialCharacter")
                                                                         : IdentityResult.Success);
        }
    }
}

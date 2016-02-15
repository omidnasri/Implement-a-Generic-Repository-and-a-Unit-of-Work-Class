namespace Karin.BaseConfig.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class RExpression
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Email = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        /// <summary>
        /// 
        /// </summary>
        public const string CharacterAndNumber = @"[A-Za-z][A-Za-z0-9._]{3,14}";

        /// <summary>
        /// 
        /// </summary>
        public const string Password = @"[A-Za-z0-9]{6,15}";
        /// <summary>
        /// 
        /// </summary>
        public const string Float = @"^[0-9]*(?:\.[0-9]*)?$";
        /// <summary>
        /// 
        /// </summary>
        public const string Number = "^[0-9]*$";

        /// <summary>
        ///  
        /// </summary>
        public const string PersianDate = @"^[1-4]\d{3}\/((0?[1-6]\/((3[0-1])|([1-2][0-9])|(0?[1-9])))|((1[0-2]|(0?[7-9]))\/(30|([1-2][0-9])|(0?[1-9]))))$";

        /// <summary>
        /// 
        /// </summary>
        public const string IsGuid = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";
    }
}

namespace Karin.BaseConfig.Config
{
    public static class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefualtLenght = "20";
        /// <summary>
        /// 
        /// </summary>
        public const string DefualtDateLenght = "10";
        /// <summary>
        /// 
        /// </summary>
        public const string DefualtPersianDateLenght = "10";
        /// <summary>
        /// 
        /// </summary>
        public const string Ntext = "ntext";
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultConnectionStringName = "DefaultConnection";
        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string RTL = "rtl";
        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const string LTR = "ltr";
        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const bool IsRTL = true;
        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const bool IsLTR = false;
        /// <summary>
        /// 
        /// </summary>
        private const int DRowCount = 100;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int RowCount()
        {
            int result;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["rowCount"], out result);
            return result.Equals(0) ? DRowCount
                                    : result;
        }
    }
}
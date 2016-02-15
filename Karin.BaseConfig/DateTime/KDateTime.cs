using System;
using System.Globalization;

namespace Karin.BaseConfig.DateTime
{
    public class KDateTime
    {
        /// <summary>
        /// Summary description for SolarDate
        /// </summary>
        
            static PersianCalendar _persianCalendar = new PersianCalendar();
            HijriCalendar _hijriCalendar = new HijriCalendar();
            private static string _currentDateTime;
            private static string _currentTime;
            private static string _currentDate;
            private static int _yAge;
            private static int _mAge;
            private static int _dAge;

            private int _yearsDifference;
            private int _monthsDifference;
            private int _daysDifference;

            /// <summary>
            /// دريافت تاريخ كامل امروز به صورت شمسي. 
            ///<example>چهارشنبه 14 دي 1389</example>
            /// </summary>
            public static string GetToday
            {
                get
                {
                    var pc = new PersianCalendar();
                    return KDateTime.CurrentDay + " " + pc.GetDayOfMonth(System.DateTime.Now) + " " + KDateTime.CurrentMonth + " " + pc.GetYear(System.DateTime.Now);
                }
            }

            #region DifferenceBetween2Date
            /// <summary>
            /// تفاضل دو تاريخ شمسي را محاسبه ميكند.
            /// و در متغيرهاي مربوطه قرار ميدهد
            /// <para>Day is in  GetDaysDifference </para>
            /// <para>Month is in GetMonthsDifference</para>
            /// <para>Year is in GetYearsDifference</para>
            /// </summary>
            /// <param name="date1">تاريخ اول yyyy/mm/dd</param>
            /// <param name="date2">تاريخ دوم yyyy/mm/dd</param>
            /// <returns>void</returns>
            public void DifferenceBetween2Date(string date1, string date2)
            {

                int _BigDay, _BigMonth, bigYear; //for greater Date 1390/10/05
                int smallDay, _SmallMonth, _SmallYear; //for greater Date 1389/02/10

                if (System.String.Compare(date1, date2, System.StringComparison.Ordinal) == 1) //date1>date2
                {
                    _BigDay = GetDayFromSolarDate(date1);
                    _BigMonth = GetMonthFromSolarDate(date1);
                    bigYear = GetYearFromSolarDate(date1);

                    smallDay = GetDayFromSolarDate(date2);
                    _SmallMonth = GetMonthFromSolarDate(date2);
                    _SmallYear = GetYearFromSolarDate(date2);


                }
                else
                {
                    _BigDay = GetDayFromSolarDate(date2);
                    _BigMonth = GetMonthFromSolarDate(date2);
                    bigYear = GetYearFromSolarDate(date2);

                    smallDay = GetDayFromSolarDate(date1);
                    _SmallMonth = GetMonthFromSolarDate(date1);
                    _SmallYear = GetYearFromSolarDate(date1);
                }

                if (_BigDay < smallDay)
                {
                    if (_BigMonth > 6)
                    {
                        _BigDay += 30;
                    }
                    else
                    {
                        _BigDay += 31;
                    }

                    if (_BigMonth == 1)
                    {
                        _BigMonth = 12;
                        bigYear -= 1;
                    }

                    _daysDifference = _BigDay - smallDay;
                }
                else
                {
                    _daysDifference = _BigDay - smallDay;
                }

                /////////////////////////////////

                if (_BigMonth < _SmallMonth)
                {
                    _BigMonth += 12;
                    bigYear -= 1;
                    _monthsDifference = _BigMonth - _SmallMonth;
                }
                else
                {
                    _monthsDifference = _BigMonth - _SmallMonth;
                }
                //////////////////////////////

                _yearsDifference = bigYear - _SmallYear;

            }
            /// <summary>
            /// تعداد سال بدست آمده از تفاضل دو تاريخ شمسي
            /// </summary>
            public int GetYearsDifference
            {
                get { return _yearsDifference; }
            }
            /// <summary>
            /// تعداد ماه بدست آمده از تفاضل دو تاريخ شمسي
            /// </summary>
            public int GetMonthsDifference
            {
                get { return _monthsDifference; }
            }
            /// <summary>
            /// تعداد روز بدست آمده از تفاضل دو تاريخ شمسي
            /// </summary>
            public int GetDaysDifference
            {
                get { return _daysDifference; }
            }

            /// <summary>
            /// روز تاريخ شمسي را ميدهد
            /// <example>
            /// <para>1389/12/24</para>
            /// <value>24</value>
            /// </example>
            /// </summary>
            /// <param name="sDate">تاريخ شمسي</param>
            /// <returns>int</returns>
            public int GetDayFromSolarDate(string sDate)
            {
                return int.Parse(sDate.Substring(8, 2));
            }

            /// <summary>
            /// ماه تاريخ شمسي را ميدهد
            /// <example>
            /// <para>1389/12/24</para>
            /// <value>12</value>
            /// </example>
            /// </summary>
            /// <param name="sDate">تاريخ شمسي</param>
            /// <returns>int</returns>
            public int GetMonthFromSolarDate(string sDate)
            {
                return int.Parse(sDate.Substring(5, 2));
            }

            /// <summary>
            /// سال تاريخ شمسي را ميدهد
            /// <example>
            /// <para>1389/12/24</para>
            /// <value>1389</value>
            /// </example>
            /// </summary>
            /// <param name="sDate">تاريخ شمسي</param>
            /// <returns>int</returns>
            public int GetYearFromSolarDate(string sDate)
            {
                return int.Parse(sDate.Substring(0, 4));
            }
            #endregion

            /// <summary>
            /// تاریخ و زمان فعلی را به صورت 
            /// یک رشته 19 کاراکتری برمی گرداند
            /// </summary>
            public static string CurrentDateTime
            {
                get
                {
                    var pc = new PersianCalendar();
                    string year = pc.GetYear(System.DateTime.Now).ToString();
                    string month = pc.GetMonth(System.DateTime.Now).ToString();
                    string day = pc.GetDayOfMonth(System.DateTime.Now).ToString();
                    string hour = pc.GetHour(System.DateTime.Now).ToString();
                    string minute = pc.GetMinute(System.DateTime.Now).ToString();
                    string second = pc.GetSecond(System.DateTime.Now).ToString();

                    if (month.Length == 1)
                        month = "0" + month;
                    if (day.Length == 1)
                        day = "0" + day;
                    if (hour.Length == 1)
                        hour = "0" + hour;
                    if (minute.Length == 1)
                        minute = "0" + minute;
                    if (second.Length == 1)
                        second = "0" + second;

                    _currentDateTime = hour + ":" + minute + ":" + second + " " + year + "/" + month + "/" + day;
                    return _currentDateTime;

                }
            }

            /// <summary>
            /// زمان فعلی را به صورت 
            /// یک رشته 8 کاراکتری برمی گرداند
            /// </summary>
            public static string CurrentTime
            {
                get
                {
                    var pc = new PersianCalendar();
                    string hour = pc.GetHour(System.DateTime.Now).ToString();
                    string minute = pc.GetMinute(System.DateTime.Now).ToString();
                    string second = pc.GetSecond(System.DateTime.Now).ToString();

                    if (hour.Length == 1)
                        hour = "0" + hour;
                    if (minute.Length == 1)
                        minute = "0" + minute;
                    if (second.Length == 1)
                        second = "0" + second;
                    _currentTime = hour + ":" + minute + ":" + second;
                    return _currentTime;
                }

            }
            public static string CurrentDay
            {
                get
                {
                    var pc = new PersianCalendar();
                    DayOfWeek d = pc.GetDayOfWeek(System.DateTime.Now);
                    if (d == DayOfWeek.Friday)
                        return "جمعه";
                    else if (d == DayOfWeek.Monday)
                        return "دوشنبه";
                    else if (d == DayOfWeek.Saturday)
                        return "شنبه";
                    else if (d == DayOfWeek.Sunday)
                        return "یکشنبه";
                    else if (d == DayOfWeek.Thursday)
                        return "پنجشنبه";
                    else if (d == DayOfWeek.Tuesday)
                        return "سه شنبه";
                    else
                        return "چهارشنبه";
                }

            }
            public static int CurrentDayNumber
            {
                get
                {
                    var pc = new PersianCalendar();
                    return pc.GetDayOfMonth(System.DateTime.Now);
                }

            }
            /// <summary>
            /// شماره ماه جاري را ميدهد
            /// </summary>
            public static int CurrentMonthNumber
            {
                get
                {
                    return new PersianCalendar().GetMonth(System.DateTime.Now);
                }
            }
            public static string CurrentMonth
            {
                get
                {
                    var pc = new PersianCalendar();
                    int d = pc.GetMonth(System.DateTime.Now);
                    if (d == 1)
                        return "فروردین";
                    else if (d == 2)
                        return "اردیبهشت";
                    else if (d == 3)
                        return "خرداد";
                    else if (d == 4)
                        return "تیر";
                    else if (d == 5)
                        return "مرداد";
                    else if (d == 6)
                        return "شهریوی";
                    else if (d == 7)
                        return "مهر";
                    else if (d == 8)
                        return "آبان";
                    else if (d == 9)
                        return "آذر";
                    else if (d == 10)
                        return "دی";
                    else if (d == 11)
                        return "بهمن";
                    else if (d == 12)
                        return "اسفند";
                    else
                        return "";
                }

            }
            /// <summary>
            /// تاریخ شمسی فعلی را به صورت 
            /// یک رشته 10 کاراکتری برمی گرداند
            /// </summary>
            public static string CurrentDate
            {
                get
                {
                    var pc = new PersianCalendar();
                    string year = pc.GetYear(System.DateTime.Now).ToString();
                    string month = pc.GetMonth(System.DateTime.Now).ToString();
                    string day = pc.GetDayOfMonth(System.DateTime.Now).ToString();
                    if (month.Length == 1)
                        month = "0" + month;
                    if (day.Length == 1)
                        day = "0" + day;
                    _currentDate = year + "/" + month + "/" + day;
                    return _currentDate;
                }
            }
            public static string GetNextWeekday(DayOfWeek _day, byte _n)
            {
                System.DateTime NextDay = System.DateTime.Today;
                for (byte i = 1; i <= _n; i++)
                {
                    NextDay = NextDay.AddDays(1);
                    int daysToAdd = ((int)_day - (int)NextDay.DayOfWeek + 7) % 7;
                    NextDay = NextDay.AddDays(daysToAdd);
                }
                var pc = new PersianCalendar();
                string year = pc.GetYear(NextDay).ToString();
                string month = pc.GetMonth(NextDay).ToString();
                string day = pc.GetDayOfMonth(NextDay).ToString();
                if (month.Length == 1)
                    month = "0" + month;
                if (day.Length == 1)
                    day = "0" + day;

                return year + "/" + month + "/" + day;
            }
            public static string GetNextday(System.DateTime _LastDate, int _days)
            {
                System.DateTime NextDay = _LastDate;
                //for (byte i = 1; i <= _n; i++)
                //{
                //    NextDay = NextDay.AddDays(1);
                //    int daysToAdd = ((int)_day - (int)NextDay.DayOfWeek + 7) % 7;

                //}
                NextDay = NextDay.AddDays(_days);
                var pc = new PersianCalendar();
                string year = pc.GetYear(NextDay).ToString();
                string month = pc.GetMonth(NextDay).ToString();
                string day = pc.GetDayOfMonth(NextDay).ToString();
                if (month.Length == 1)
                    month = "0" + month;
                if (day.Length == 1)
                    day = "0" + day;

                return year + "/" + month + "/" + day;
            }
            public static string CurrentYear
            {
                get
                {
                    return new PersianCalendar().GetYear(System.DateTime.Now).ToString();
                }
            }

            ///// <summary>
            ///// تغيير حالت صحفه كليد به فارسي
            ///// </summary>
            //public static void ChangeKeyboardLayout2Farsi()
            //{
            //    try
            //    {
            //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa-IR");
            //        foreach (System.Windows.Forms.InputLanguage lang in System.Windows.Forms.InputLanguage.InstalledInputLanguages)
            //        {
            //            if (lang.Handle.ToString() == "69796905" ||
            //                lang.LayoutName.ToUpper() == "FARSI")
            //                System.Windows.Forms.InputLanguage.CurrentInputLanguage = lang;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}

            /*public static string CalculateAge(string date)
        {
            date = validDate(date);
            if (date != "####")
            {
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(8, 2));
                DateTime birthDate = GregorianDate(year, month, day, 0, 0, 0, 0);
                SqlCommand cmd = new SqlCommand("AgeCalculate");
                cmd.Parameters.AddWithValue("@START_DATE", birthDate);
                cmd.Parameters.AddWithValue("@END_DATE", DateTime.Now);
                string age = new DatabaseManager().ExecuteScalar(cmd).ToString();
                return int.Parse(age.Substring(0, 4)) + " " + "سال" + " " + int.Parse(age.Substring(5, 2)) + " " + "ماه" + " " + int.Parse(age.Substring(8, 2)) + " " + "روز";
            }
            else
                return "";
        }*/
            /*public static int CalculateAgeYear(string date)
        {
            date = validDate(date);
            if (date != "####")
            {
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(8, 2));
                DateTime birthDate = GregorianDate(year, month, day, 0, 0, 0, 0);
                SqlCommand cmd = new SqlCommand("AgeCalculate");
                cmd.Parameters.AddWithValue("@START_DATE", birthDate);
                cmd.Parameters.AddWithValue("@END_DATE", DateTime.Now);
                string age = new DatabaseManager().ExecuteScalar(cmd).ToString();
                return int.Parse(age.Substring(0, 4));
            }
            else
                return 0;
        }*/
            /*public static int CalculateAgeMonth(string date)
        {
            date = validDate(date);
            if (date != "####")
            {
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(5, 2));
                int day = int.Parse(date.Substring(8, 2));
                DateTime birthDate = GregorianDate(year, month, day, 0, 0, 0, 0);
                SqlCommand cmd = new SqlCommand("AgeCalculate");
                cmd.Parameters.AddWithValue("@START_DATE", birthDate);
                cmd.Parameters.AddWithValue("@END_DATE", DateTime.Now);
                string age = new DatabaseManager().ExecuteScalar(cmd).ToString();
                return int.Parse(age.Substring(5, 2));
            }
            else
                return 0;
        }*/
            #region
            /// <summary>
            /// بررسی می کند که کاراکتر غیر عددی در رشته نباشد
            /// </summary>
            /// <param name="str"> رشته ورودی روز ،ماه یا سال است</param>
            /// <returns>را برمیگرداند true در صورت عدم وجود کاراکتر نامعتبر مقدار </returns>
            private static bool isNumber(string str)
            {
                int count = 0;
                for (int i = 0; i < str.Length; i++)
                    if (str.ToCharArray()[i] >= '0' && str.ToCharArray()[i] <= '9')
                        count++;
                    else
                        return false;

                if (count == 2 || count == 4)
                    return true;
                return false;
            }
            private static void MonthLessmonth(int D, int M, int Y, int d, int m, int y)
            {
                PersianCalendar pc = new PersianCalendar();
                _mAge = ((M - 1) + 12) - m;
                _yAge = (Y - 1) - y;
                if (pc.IsLeapYear(Y) && pc.IsLeapYear(y))
                {
                    if (M >= 1 && M <= 6)
                    {
                        _dAge = (D + 31) - d;

                        if (m > 1 && m <= 6)
                            _dAge += (_yAge / 4) + 1;

                        else if (m >= 7 && m <= 11)
                            _dAge += (_yAge / 4);

                        if (m == 12)
                            _dAge += (_yAge / 4) - 1;

                    }
                    ///////////////////////
                    if (M >= 7 && M <= 11)
                    {
                        _dAge = (D + 30) - d;

                        if (m > 7 && m <= 11)
                            _dAge += (_yAge / 4) + 1;

                        if (m == 12)
                            _dAge += (_yAge / 4);
                    }

                }
                //////////////////////////////////////////////
                if (!pc.IsLeapYear(Y) && pc.IsLeapYear(y))
                {
                    if (M >= 1 && M <= 6)
                    {
                        _dAge = (D + 31) - d;

                        if (m >= 1 && m <= 6)
                            _dAge += (_yAge / 4) + 1;

                        if (m >= 7 && m <= 12)
                            _dAge += (_yAge / 4);

                    }
                    if (M >= 7 && M <= 11)
                    {
                        _dAge = (D + 30) - d;

                        if (m >= 7 && m <= 11)
                            _dAge += (_yAge / 4) + 1;

                        if (m == 12)
                            _dAge += (_yAge / 4);
                    }
                }
                ///////////////////////////////////////////
                if (!pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
                {
                    if (M >= 1 && M <= 6)
                    {
                        _dAge = (D + 31) - d;

                        if (m >= 1 && m <= 6)
                            _dAge += (_yAge / 4);

                        else if (m >= 7 && m <= 11)
                            _dAge += (_yAge / 4) - 1;

                        else if (m == 12)
                            _dAge += (_yAge / 4) - 2;

                    }
                    else if (M >= 7 && M <= 11)
                    {
                        _dAge = (D + 30) - d;

                        if (m >= 7 && m <= 11)
                            _dAge += _yAge / 4;

                        if (m == 12)
                            _dAge += (_yAge / 4) - 1;

                    }
                }
                //////////////////////////////////////////
                if (pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
                {
                    if (M >= 1 && M <= 6)
                    {
                        _dAge = (D + 31) - d;

                        if (m >= 1 && m <= 6)
                            _dAge += (_yAge / 4);

                        if (m >= 7 && m <= 11)
                            _dAge += (_yAge / 4) - 1;

                        if (m == 12)
                            _dAge += (_yAge / 4) - 2;
                    }
                    ///////////////////////
                    if (M >= 7 && M <= 11)
                    {
                        _dAge = (D + 30) - d;

                        if (m >= 7 && m <= 11)
                            _dAge += _yAge / 4;

                        if (m == 12)
                            _dAge += (_yAge / 4) - 1;
                    }
                }
            }
            private static void MonthMostmonth(int D, int M, int Y, int d, int m, int y)
            {
                var pc = new PersianCalendar();
                _mAge = (M - 1) - m;
                _yAge = Y - y;
                if ((pc.IsLeapYear(Y) && !pc.IsLeapYear(y)) || (pc.IsLeapYear(Y) && pc.IsLeapYear(y)) || (!pc.IsLeapYear(Y) && !pc.IsLeapYear(y)))
                {
                    if (M >= 1 && M <= 6)
                    {
                        _dAge = (D + 31) - d;
                        _dAge += _yAge / 4;
                    }
                }
                if (!pc.IsLeapYear(Y) && pc.IsLeapYear(y))
                {
                    if (M >= 1 && M <= 6)
                    {
                        _dAge = (D + 31) - d;
                        _dAge += (_yAge / 4) + 1;
                    }
                    else if (M >= 7 && M <= 11)
                    {
                        _dAge = (D + 30) - d;
                        _dAge += (_yAge / 4) + 2;
                    }
                    else if (M == 12)
                    {
                        _dAge = (D + 29) - d;
                        _dAge += (_yAge / 4) + 3;
                    }
                }
                if ((pc.IsLeapYear(Y) && !pc.IsLeapYear(y)) || (pc.IsLeapYear(Y) && pc.IsLeapYear(y)))
                {
                    if (M >= 7 && M <= 12)
                    {
                        _dAge = (D + 30) - d;
                        _dAge += (_yAge / 4) + 1;
                    }
                }
                if (!pc.IsLeapYear(Y) && !pc.IsLeapYear(y))
                {
                    if (M >= 7 && M <= 11)
                    {
                        _dAge = (D + 30) - d;
                        _dAge += (_yAge / 4) + 1;
                    }
                    else if (M == 12)
                    {
                        _dAge = (D + 29) - d;
                        _dAge += (_yAge / 4) + 2;
                    }
                }
            }
            private static void MonthEqualemonth(int D, int M, int Y, int d, int m, int y)
            {
                PersianCalendar pc = new PersianCalendar();
                _mAge = ((M - 1) + 12) - m;
                _yAge = (Y - 1) - y;

                if (M >= 1 && M <= 6)
                    _dAge = (D + 31) - d;

                else if (M >= 7 && M <= 11)
                    _dAge = (D + 30) - d;

                else if (M == 12)
                {
                    if (pc.IsLeapYear(Y))
                        _dAge = (D + 30) - d;

                    else
                        _dAge = (D + 29) - d;
                }
                if (pc.IsLeapYear(y) && !pc.IsLeapYear(Y))
                    _dAge += (_yAge / 4) + 1;

                if ((!pc.IsLeapYear(Y) && !pc.IsLeapYear(y)) || (pc.IsLeapYear(Y) && !pc.IsLeapYear(y)))
                    _dAge += (_yAge / 4);

                if (pc.IsLeapYear(Y) && pc.IsLeapYear(y))
                    _dAge += (_yAge / 4) + 1;
            }
            #endregion
            /// <summary>
            /// دو تاریخ را با هم مقایسه می کند و اگر پارامتر اول بزرگتر باشد عدد 1 ،پارامتر دوم بزرگتر باشد عدد -1 و اگر باهم برابر باشند 0 را بر می گرداند و در صورتی که فرمت هر کدام از تاریخ ها نادرست باشد عدد غیر از این اعداد را برمیگرداند 
            /// </summary>
            public static int compare(string date1, string date2)
            {
                date1 = validDate(date1);
                date2 = validDate(date2);
                if (date1 == "####" || date2 == "####")
                    return -100;

                string year1 = date1.Substring(0, date1.IndexOf('/'));
                string month1 = date1.Substring(date1.IndexOf('/') + 1, (date1.LastIndexOf('/') - year1.Length - 1));
                string day1 = date1.Substring(date1.LastIndexOf('/') + 1, (date1.Length - year1.Length) - month1.Length - 2);

                string year2 = date2.Substring(0, date2.IndexOf('/'));
                string month2 = date2.Substring(date2.IndexOf('/') + 1, (date2.LastIndexOf('/') - year2.Length - 1));
                string day2 = date2.Substring(date2.LastIndexOf('/') + 1, (date2.Length - year2.Length) - month2.Length - 2);
                if (year1.CompareTo(year2) > 0)
                    return 1;
                else if (year1.CompareTo(year2) < 0)
                    return -1;
                else if (year2 == year1)
                {
                    if (month1.CompareTo(month2) > 0)
                        return 1;
                    else if (month1.CompareTo(month2) < 0)
                        return -1;
                    else if (month1 == month2)
                    {
                        if (day1.CompareTo(day2) > 0)
                            return 1;
                        else if (day1.CompareTo(day2) < 0)
                            return -1;
                        else if (day1 == day2)
                        {
                            return 0;
                        }
                    }
                }
                return 100;
            }
            public static string validDate(string s)
            {
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                        if (s.ToCharArray()[l] != '/')
                        {
                            s = s.Remove(l, 1);
                            l = 0;
                        }
                if (s.ToCharArray()[s.Length - 1] == '/')
                    s = s.Remove(s.Length - 1, 1);
                if (s.ToCharArray()[s.Length - 1] == '/')
                    s = s.Remove(s.Length - 1, 1);
                if (s.Length == 4)
                    s = s + "/01/01";
                if (s.Length == 2)
                    s = s + "/01/01";
                int count = 0;
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] == '/')
                        count++;
                if (count == 1)
                {
                    s = s + "/01";
                    count++;
                }
                if (s.Length < 6 || count != 2)
                    return "####";


                string year = s.Substring(0, s.IndexOf('/'));
                string month = s.Substring(s.IndexOf('/') + 1, (s.LastIndexOf('/') - year.Length - 1));
                string day = s.Substring(s.LastIndexOf('/') + 1, (s.Length - year.Length) - month.Length - 2);

                if (year.Length == 2)
                    year = "13" + year;
                else if (year.Length != 4)
                    return "####";

                if (month.Length == 1)
                    month = "0" + month;
                else if (month.Length != 2)
                    return "####";

                if (day.Length == 1)
                    day = "0" + day;
                else if (day.Length != 2)
                    return "####";
                return year + "/" + month + "/" + day;
            }
            /// <summary>
            /// رشته ای را گرفته و آنرا بررسی می کند در صورتی که این رشته یک تاریخ معتبر باشد مقدار 
            /// OK
            /// و در غیر اینصورت خطای آنرا بر می گرداند
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public static string validateBirthDate(string s)
            {
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                        if (s.ToCharArray()[l] != '/')
                        {
                            s = s.Remove(l, 1);
                            l = 0;
                        }
                if (s.ToCharArray()[s.Length - 1] == '/')
                    s = s.Remove(s.Length - 1, 1);
                if (s.ToCharArray()[s.Length - 1] == '/')
                    s = s.Remove(s.Length - 1, 1);
                if (s.Length == 4)
                    s = s + "/01/01";
                if (s.Length == 2)
                    s = s + "/01/01";
                int count = 0;
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] == '/')
                        count++;
                if (count == 1)
                {
                    s = s + "/01";
                    count++;
                }
                if (s.Length < 6 || count != 2)
                    return "این تاریخ تولد معتبر نمی باشد";
                string year = s.Substring(0, s.IndexOf('/'));
                string month = s.Substring(s.IndexOf('/') + 1, (s.LastIndexOf('/') - year.Length - 1));
                string day = s.Substring(s.LastIndexOf('/') + 1, (s.Length - year.Length) - month.Length - 2);

                if (year.Length == 2)
                    year = "13" + year;
                else if (year.Length != 4)
                    return "سال تولد معتبر نیست";
                if (int.Parse(year) < 1250)
                    return "سال این تاریخ تولد نامعتبر است";
                if (int.Parse(month) > 12 || int.Parse(month) < 1)
                    return "ماه این تاریخ تولد نامعتبر است";
                if (month.Length == 1)
                    month = "0" + month;
                else if (month.Length != 2)
                    return "ماه تاریخ تولد نامعتبر می باشد";

                if (day.Length == 1)
                    day = "0" + day;
                if (int.Parse(day) > 31 || int.Parse(day) < 1)
                    return "روز تاریخ تولد معتبر نمی باشد";
                if (int.Parse(day) > 30 && int.Parse(month) > 6)
                    return "روز تاریخ تولد معتبر نمی باشد";
                else if (day.Length != 2)
                    return "روز تاریخ تولد معتبر نمی باشد";
                if (KDateTime.compare(year + "/" + month + "/" + day, KDateTime.CurrentDate) == 1)
                    return "تاریخ تولد نباید از تاریخ فعلی بزرگتر باشد";
                if (int.Parse(day) == 30 && int.Parse(month) == 12 && !new PersianCalendar().IsLeapYear(int.Parse(year)))
                    return "سال وارد شده کبیسه نیست";
                return "OK";
            }
            public static System.DateTime GregorianDate(int year, int month, int day, int hour, int minute, int second, int milisecond)
            {
                var p = new PersianCalendar();
                return p.ToDateTime(year, month, day, hour, minute, second, milisecond);
            }
            public static string validateDate(string s)
            {
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                        if (s.ToCharArray()[l] != '/')
                            return "این تاریخ معتبر نمی باشد";
                int count = 0;
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] == '/')
                        count++;
                if (s.Length != 10 || count != 2)
                    return "این تاریخ معتبر نمی باشد";
                string year = s.Substring(0, s.IndexOf('/'));
                string month = s.Substring(s.IndexOf('/') + 1, (s.LastIndexOf('/') - year.Length - 1));
                string day = s.Substring(s.LastIndexOf('/') + 1, (s.Length - year.Length) - month.Length - 2);

                if (year.Length != 4)
                    return "سال معتبر نیست";
                if (int.Parse(year) < 1250)
                    return "سال این تاریخ ، نامعتبر است";
                if (int.Parse(month) > 12 || int.Parse(month) < 1 || month.Length != 2)
                    return "ماه این تاریخ ، نامعتبر است";
                if (int.Parse(day) > 31 || int.Parse(day) < 1 || day.Length != 2)
                    return "روز تاریخ ، معتبر نمی باشد";
                if (int.Parse(day) > 30 && int.Parse(month) > 6)
                    return "روز تاریخ ، معتبر نمی باشد";
                if (KDateTime.compare(year + "/" + month + "/" + day, KDateTime.CurrentDate) == 1)
                    return "تاریخ ، نباید از تاریخ فعلی بزرگتر باشد";
                if (int.Parse(day) == 30 && int.Parse(month) == 12 && !new PersianCalendar().IsLeapYear(int.Parse(year)))
                    return "سال وارد شده کبیسه نیست";
                return "OK";
            }
            public static string validateDate2(string s)
            {
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] < '0' || s.ToCharArray()[l] > '9')
                        if (s.ToCharArray()[l] != '/')
                            return "این تاریخ معتبر نمی باشد";
                int count = 0;
                for (int l = 0; l < s.Length; l++)
                    if (s.ToCharArray()[l] == '/')
                        count++;
                if (s.Length != 10 || count != 2)
                    return "این تاریخ معتبر نمی باشد";
                string year = s.Substring(0, s.IndexOf('/'));
                string month = s.Substring(s.IndexOf('/') + 1, (s.LastIndexOf('/') - year.Length - 1));
                string day = s.Substring(s.LastIndexOf('/') + 1, (s.Length - year.Length) - month.Length - 2);

                if (year.Length != 4)
                    return "سال معتبر نیست";
                if (int.Parse(year) < 1250)
                    return "سال این تاریخ ، نامعتبر است";
                if (int.Parse(month) > 12 || int.Parse(month) < 1 || month.Length != 2)
                    return "ماه این تاریخ ، نامعتبر است";
                if (int.Parse(day) > 31 || int.Parse(day) < 1 || day.Length != 2)
                    return "روز تاریخ ، معتبر نمی باشد";
                if (int.Parse(day) > 30 && int.Parse(month) > 6)
                    return "روز تاریخ ، معتبر نمی باشد";
                if (int.Parse(day) == 30 && int.Parse(month) == 12 && !new PersianCalendar().IsLeapYear(int.Parse(year)))
                    return "سال وارد شده کبیسه نیست";
                return "OK";
            }
            public static string validateTime(string s)
            {
                if (s.Length != 5 || s.ToCharArray()[2] != ':')
                    return "این زمان معتبر نمی باشد";
                if (s.ToCharArray()[0] > '9' || s.ToCharArray()[0] < '0' || s.ToCharArray()[1] > '9' || s.ToCharArray()[1] < '0' || s.ToCharArray()[3] > '9' || s.ToCharArray()[3] < '0' || s.ToCharArray()[4] > '9' || s.ToCharArray()[4] < '0')
                    return "این زمان معتبر نمی باشد";
                string hour = s.Substring(0, s.IndexOf(':'));
                string menute = s.Substring(s.IndexOf(':') + 1, 2);
                if (int.Parse(hour) < 0 || int.Parse(hour) > 23)
                    return "ساعت این زمان ، نامعتبر است";
                else if (int.Parse(menute) < 0 || int.Parse(menute) > 59)
                    return "دقیقه این زمان ، نامعتبر است";
                return "OK";
            }
            public static int Cabiseh_No(int year)
            {
                int Y, Y1, Y2, Y3, Y4, CABISE_NO;
                Y = (year - 508 + 1300);
                Y1 = Y / 128;
                Y2 = Y % 128;
                Y3 = Y2 / 33;
                Y4 = Y2 % 33;
                CABISE_NO = (Y1 * 31 + Y3 * 8 + (Y4 / 4) - (Y2 / 127) - (Y4 / 32) + 123 - 314);
                return CABISE_NO;

            }
            public static string Shamsi2Miladi(string shamsi_date)
            {
                string SHAMSI_DATE = shamsi_date;
                string MILADI_DATE;
                int YEAR, MONTH, DAY, TEMP, CABISE_NO, TEMPYEAR, TEMPMONTH, TEMPDAY;

                YEAR = Convert.ToInt32(SHAMSI_DATE.Substring(0, 4));

                MONTH = Convert.ToInt32(SHAMSI_DATE.Substring(5, 2));

                DAY = Convert.ToInt32(SHAMSI_DATE.Substring(8, 2));


                if ((YEAR > 65 || YEAR < 99) && (MONTH >= 1 || MONTH <= 12) && (DAY >= 1 || DAY <= 31))
                    return "";


                YEAR--;
                CABISE_NO = Cabiseh_No(YEAR);

                YEAR++;

                TEMP = YEAR * 365 + CABISE_NO;
                if (MONTH <= 6)
                    TEMP = TEMP + (MONTH - 1) * 31;

                if ((MONTH <= 11) && (MONTH > 6))
                    TEMP = TEMP + 6 * 31 + (MONTH - 7) * 30;

                if (MONTH == 12)
                    TEMP = TEMP + 6 * 31 + 5 * 30;

                TEMP = TEMP + DAY;
                TEMP = TEMP + 7748;

                TEMPYEAR = int.Parse(TEMP.ToString().Substring(0, 4));
                TEMPMONTH = int.Parse(TEMP.ToString().Substring(5, 2));
                TEMPDAY = int.Parse(TEMP.ToString().Substring(8, 2));

                MILADI_DATE = TEMPYEAR.ToString();

                if (TEMPMONTH < 10)
                    MILADI_DATE = MILADI_DATE + "/0" + TEMPMONTH.ToString();
                else
                    MILADI_DATE = MILADI_DATE + "/" + TEMPMONTH.ToString();

                if (TEMPDAY < 10)
                    MILADI_DATE = MILADI_DATE + "/0" + TEMPDAY.ToString();
                else
                    MILADI_DATE = MILADI_DATE + "/" + TEMPDAY.ToString();

                return MILADI_DATE;

            }
            //*********************************



            //    CREATE       Function FnCalenMiladi2Shamsi 
            //    (@MILADI_DATE  varchar(10))
            //RETURNS varchar(10)
            //AS
            //BEGIN
            //--DECLARE @MILADI_DATE varchar(10)
            //DECLARE @SHAMSI_DATE varchar(10)
            //DECLARE @MILADI_DATETIME INT,
            //        @YEAR INT,
            //        @MONTH INT,
            //        @DAY INT,
            //        @CABISE_NO INT,
            //        @CABISE_NO_1 INT,
            //        @RETURN_VALUE INT
            //-- CONVERT DATE TO INT
            //SET @MILADI_DATETIME = CONVERT(INT, CONVERT(DATETIME, @MILADI_DATE)) 
            //-- CALCULATE BEGINING OF DATE
            //SET @MILADI_DATETIME = @MILADI_DATETIME - 7748
            //SET @YEAR = @MILADI_DATETIME / 365
            //--CALCULATE CABISE
            //SET @YEAR = @YEAR - 1
            //set @RETURN_VALUE = dbo.FnCalenCabiseh_No(@YEAR)
            //SET @YEAR = @YEAR + 1
            //IF @RETURN_VALUE < 0 
            // set @CABISE_NO = dbo.FnCalenCabiseh_No(@YEAR)
            //ELSE 
            //BEGIN
            // SET @YEAR = @YEAR - 1 
            // set @CABISE_NO = dbo.FnCalenCabiseh_No(@YEAR)
            // SET @YEAR = @YEAR + 1 
            //END
            //SET @DAY = @MILADI_DATETIME - (@YEAR * 365 + @CABISE_NO)
            //IF @DAY <= 0 
            //BEGIN
            //  SET @YEAR = @YEAR - 2
            //  set @CABISE_NO = dbo.FnCalenCabiseh_No(@YEAR)
            //  SET @YEAR = @YEAR + 1
            //  SET @DAY = @MILADI_DATETIME - (@YEAR * 365 + @CABISE_NO) 
            //END
            //SET @MONTH = 1
            //WHILE (@MONTH <= 6 AND @DAY > 31) OR
            //      (@MONTH > 6 AND @DAY > 30 AND @MONTH <= 11) OR
            //      (@RETURN_VALUE = 1 AND @DAY > 30 AND @MONTH = 12) OR
            //      (@RETURN_VALUE = 0 AND @DAY > 29 AND @MONTH = 12) 
            //BEGIN
            //  IF @MONTH <= 6 
            //    SET @DAY = @DAY - 31
            //  ELSE IF @MONTH <= 11
            //    SET @DAY = @DAY - 30
            //  ELSE
            //  BEGIN
            //    IF @RETURN_VALUE = 1 
            //      SET @DAY = @DAY - 30
            //    ELSE 
            //      SET @DAY = @DAY - 29
            //  END
            //  SET @MONTH = @MONTH + 1
            //END
            //SET @SHAMSI_DATE = CONVERT(CHAR(2), @YEAR)
            //IF @MONTH < 10 
            //  SET @SHAMSI_DATE = @SHAMSI_DATE + '/' + '0' + CONVERT(CHAR(1), @MONTH)
            //ELSE
            //  SET @SHAMSI_DATE = @SHAMSI_DATE + '/' + CONVERT(CHAR(2), @MONTH)
            //IF @DAY < 10 
            //  SET @SHAMSI_DATE = @SHAMSI_DATE + '/' + '0' + CONVERT(CHAR(1), @DAY)
            //ELSE
            //  SET @SHAMSI_DATE = @SHAMSI_DATE + '/' + CONVERT(CHAR(2), @DAY)
            //RETURN '13'+ @SHAMSI_DATE 
            //END

            /// <summary>
            /// تفاوت دو تاريخ ميلادي را به سال ماه و روز ميدهد
            /// </summary>
            public class DateDifference
            {
                /// <summary>
                /// defining Number of days in month; index 0=> january and 11=> December
                /// february contain either 28 or 29 days, that's why here value is -1
                /// which wil be calculate later.
                /// </summary>
                private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

                /// <summary>
                /// contain from date
                /// </summary>
                private System.DateTime fromDate;

                /// <summary>
                /// contain To Date
                /// </summary>
                private System.DateTime toDate;

                /// <summary>
                /// this three variable for output representation..
                /// </summary>
                private int year;
                private int month;
                private int day;

                public DateDifference(System.DateTime d1, System.DateTime d2)
                {
                    int increment;

                    if (d1 > d2)
                    {
                        this.fromDate = d2;
                        this.toDate = d1;
                    }
                    else
                    {
                        this.fromDate = d1;
                        this.toDate = d2;
                    }

                    /// 
                    /// Day Calculation
                    /// 
                    increment = 0;

                    if (this.fromDate.Day > this.toDate.Day)
                    {
                        increment = this.monthDay[this.fromDate.Month - 1];

                    }
                    /// if it is february month
                    /// if it's to day is less then from day
                    if (increment == -1)
                    {
                        if (System.DateTime.IsLeapYear(this.fromDate.Year))
                        {
                            // leap year february contain 29 days
                            increment = 29;
                        }
                        else
                        {
                            increment = 28;
                        }
                    }
                    if (increment != 0)
                    {
                        day = (this.toDate.Day + increment) - this.fromDate.Day;
                        increment = 1;
                    }
                    else
                    {
                        day = this.toDate.Day - this.fromDate.Day;
                    }

                    ///
                    ///month calculation
                    ///
                    if ((this.fromDate.Month + increment) > this.toDate.Month)
                    {
                        this.month = (this.toDate.Month + 12) - (this.fromDate.Month + increment);
                        increment = 1;
                    }
                    else
                    {
                        this.month = (this.toDate.Month) - (this.fromDate.Month + increment);
                        increment = 0;
                    }

                    ///
                    /// year calculation
                    ///
                    this.year = this.toDate.Year - (this.fromDate.Year + increment);

                }
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    //return base.ToString();
                    return this.year + " Year(s), " + this.month + " month(s), " + this.day + " day(s)";
                }
                /// <summary>
                /// 
                /// </summary>
                public int Years
                {
                    get
                    {
                        return this.year;
                    }
                }
                /// <summary>
                /// 
                /// </summary>
                public int Months
                {
                    get
                    {
                        return this.month;
                    }
                }
                /// <summary>
                /// 
                /// </summary>
                public int Days
                {
                    get
                    {
                        return this.day;
                    }
                }

            }//DateDifference

            //DateTime datetime = new DateTime();

            //تبدیل تاریخ میلادی به شمسی و قمری
            //if (comboBox1.Text != "" & comboBox1.Text != "" & comboBox1.Text != "" & radioButton3.Checked)
            //{
            //    date1 = comboBox3.Text + "/" + comboBox2.Text + "/" + comboBox1.Text;
            //    DateTime dt = DateTime.Parse(date1);
            //    label1.Text = pc.GetYear(dt) + "/" + pc.GetMonth(dt) + "/" + pc.GetDayOfMonth(dt);
            //    label9.Text = hc.GetYear(dt) + "/" + hc.GetMonth(dt) + "/" + hc.GetDayOfMonth(dt);
            //    label10.Text = dt.Year + "/" + dt.Month + "/" + dt.Day;
            //}
            ////

            ////تبدیل تاریخ قمری به شمسی و میلادی
            //if (comboBox1.Text != "" & comboBox1.Text != "" & comboBox1.Text != "" & radioButton2.Checked)
            //{
            //    date1 = comboBox3.Text + "/" + comboBox2.Text + "/" + comboBox1.Text;
            //    DateTime dt2 = hc.ToDateTime(int.Parse(comboBox3.Text), int.Parse(comboBox2.Text), int.Parse(comboBox1.Text), 0, 0, 0, 0);
            //    label1.Text = pc.GetYear(dt2) + "/" + pc.GetMonth(dt2) + "/" + pc.GetDayOfMonth(dt2);
            //    label9.Text = date1;
            //    label10.Text = dt2.Year + "/" + dt2.Month + "/" + dt2.Day;
            //}
            ////تبدیل تاریخ شمسی به میلادی و قمری
            //if (comboBox1.Text != "" & comboBox1.Text != "" & comboBox1.Text != "" & radioButton1.Checked)
            //{
            //    date1 = comboBox3.Text + "/" + comboBox2.Text + "/" + comboBox1.Text;
            //    DateTime dt1 = pc.ToDateTime(int.Parse(comboBox3.Text), int.Parse(comboBox2.Text), int.Parse(comboBox1.Text), 0, 0, 0, 0);
            //    label1.Text = date1;
            //    label9.Text = hc.GetYear(dt1) + "/" + hc.GetMonth(dt1) + "/" + hc.GetDayOfMonth(dt1);
            //    label10.Text = dt1.Year + "/" + dt1.Month + "/" + dt1.Day;
            //}
            ////
            /// <summary>
            /// Change input string datetime to miladi datetime type 
            /// </summary>
            /// <param name="datetime"></param>
            /// <returns></returns> 
            public static System.DateTime ShamsiToMiladi(System.DateTime datetime)
            {
                return _persianCalendar.ToDateTime(datetime.Year
                                                 , datetime.Month
                                                 , datetime.Day
                                                 , datetime.Hour
                                                 , datetime.Minute
                                                 , datetime.Second
                                                 , datetime.Millisecond);
            }
            /// <summary>
            /// Change input string datetime to miladi datetime type 
            /// </summary>
            /// <param name="datetime"></param>
            /// <returns></returns> 
            public static System.DateTime MiladiToShamsi(System.DateTime datetime)
            {
                var year = _persianCalendar.GetYear(datetime);
                var month = _persianCalendar.GetMonth(datetime);
                var day = _persianCalendar.GetDayOfMonth(datetime);
                var hour = _persianCalendar.GetHour(datetime);
                var minute = _persianCalendar.GetMinute(datetime);
                var second = _persianCalendar.GetSecond(datetime);
                var millisecond = (int)_persianCalendar.GetMilliseconds(datetime);
                try
                {
                    var da = new System.DateTime(year, month, day, hour, minute, second, millisecond);
                }
                catch
                {
                    ;
                }
                return new System.DateTime(year, month, day, hour, minute, second, millisecond);
            }
        }
    }
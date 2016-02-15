using System;
using System.Text;

namespace Karin.BaseConfig.Security.Encryption
{
    /// <summary>
    /// 字符串编码/解码
    /// </summary>
    public static class StringEncoder
    {
        internal const string CODE_CHARS = "12345678abcdefghijklmnpqrstuvwxy";
        internal const byte BIT5 = 0x5;
        internal const byte BIT8 = 0x8;
        private static char[] char_table;
        /// <summary>
        /// 使用指定的字符集对给定的字符串编码,默认忽略待编码字符串的大小写
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <param name="encoding">指定编码</param>
        /// <returns>string</returns>
        public static string Encode(string str, Encoding encoding)
        {
            return Encode(str, encoding, true);
        }
        /// <summary>
        /// 使用UTF8字符集对给定的字符串编码
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <param name="ignoreCase">指定是否忽略大小写</param>
        /// <returns>string</returns>
        public static string Encode(string str, bool ignoreCase)
        {
            return Encode(str, Encoding.UTF8, true);
        }
        /// <summary>
        /// 使用UTF8字符集对给定的字符串编码,默认忽略待编码字符串的大小写
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <returns>string</returns>
        public static string Encode(string str)
        {
            return Encode(str, Encoding.UTF8, true);
        }
        /// <summary>
        /// 对已编码的字符串进行解码，默认UTF8字符集
        /// </summary>
        /// <param name="str">待解码字符串</param>
        /// <returns>string</returns>
        public static string Decode(string str)
        {
            return Decode(str, Encoding.UTF8);
        }
        /// <summary>
        /// 使用指定的字符集对给定的字符串编码，并可指定是否忽略待编码字符串的大小写
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <param name="encoding">指定编码</param>
        /// <param name="ignoreCase">指定是否忽略大小写</param>
        /// <returns>string</returns>
        public static string Encode(string str, Encoding encoding, bool ignoreCase)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            if (ignoreCase)
            {
                str = str.ToLower();
            }
            StringBuilder binaryString = new StringBuilder();
            byte[] strBytes = encoding.GetBytes(str);
            foreach (byte b in strBytes)
            {
                binaryString.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            int blocks = binaryString.Length % BIT5 == 0 ? binaryString.Length / BIT5 : binaryString.Length / BIT5 + 1;
            string[] bit5Array = new string[blocks];
            int readSize = 0;
            for (int i = 0; i < blocks; i++)
            {
                readSize = BIT5;
                if (i * BIT5 + BIT5 > binaryString.Length)
                {
                    readSize = binaryString.Length - i * BIT5;
                }
                bit5Array[i] = binaryString.ToString(i * BIT5, readSize);
                bit5Array[i] = bit5Array[i].PadRight(BIT5, '0');
                bit5Array[i] = bit5Array[i].PadLeft(BIT8, '0');
            }
            byte[] encodedBytes = new byte[bit5Array.Length];
            for (int i = 0; i < bit5Array.Length; i++)
            {
                encodedBytes[i] = Convert.ToByte(bit5Array[i], 2);
            }
            if (char_table == null)
            {
                char_table = CODE_CHARS.ToCharArray();
            }
            StringBuilder encodedString = new StringBuilder();
            foreach (byte b in encodedBytes)
            {
                encodedString.Append(char_table[b]);
            }
            return encodedString.ToString();
        }
        /// <summary>
        /// 对已编码的字符串进行解码
        /// </summary>
        /// <param name="str">待解码字符串</param>
        /// <param name="encoding">编码时使用的编码</param>
        /// <returns>string</returns>
        public static string Decode(string str, Encoding encoding)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.ToLower();
            StringBuilder binaryString = new StringBuilder();
            foreach (char c in str.ToCharArray())
            {
                binaryString.Append(Convert.ToString(CODE_CHARS.IndexOf(c), 2).PadLeft(BIT8, '0'));
            }
            int n = binaryString.Length / BIT8;
            string[] bit8Array = new string[n];
            for (int i = 0; i < n; i++)
            {
                bit8Array[i] = binaryString.ToString(i * BIT8, BIT8).Substring(BIT8 - BIT5);
            }
            string bit8String = String.Join("", bit8Array);
            bit8Array = new string[bit8String.Length / BIT8];
            for (int i = 0; i < bit8Array.Length; i++)
            {
                bit8Array[i] = bit8String.Substring(i * BIT8, BIT8);
            }
            byte[] decodedBytes = new byte[bit8Array.Length];
            for (int i = 0; i < decodedBytes.Length; i++)
            {
                decodedBytes[i] = Convert.ToByte(bit8Array[i], 2);
            }
            return encoding.GetString(decodedBytes);
        }
    }
}

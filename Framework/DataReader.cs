using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework
{
    public class DataReader
    {
        public static string GetString(object input)
        {
            try
            {
                return (input.ToString());
            }
            catch { return ""; }
        }
        public static string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.UtcNow.Ticks);
        }
        public static string GetString(object input, bool dbCheck)
        {
            try
            {
                string str = GetString(input);
                return str;
                //return str.Replace("'", "''").Replace("\\", "").Replace("/*", "").Replace("*/", "");
            }
            catch { return ""; }
        }

        public static bool GetBoolean(object input)
        {
            try { return (Convert.ToBoolean(input)); }
            catch { return false; }
        }

        public static bool? GetBooleanNullable(object input)
        {
            try
            {
                return (Convert.ToBoolean(input));
            }
            catch
            {
                return null;
            }
        }

        public static Int32 GetInt32(object input)
        {
            try { return Int32.Parse(input.ToString()); }
            catch { return 0; }
        }

        public static Int32 GetInt32(object input, int defaultValue)
        {
            try { return Int32.Parse(input.ToString()); }
            catch { return defaultValue; }
        }

        public static Double GetDouble(object input)
        {
            try { return Convert.ToDouble(input.ToString()); }
            catch { return 0; }
        }

        public static DateTime GetDateTime(object input)
        {
            try { return (DateTime.Parse(input.ToString())); }
            catch
            {
                return GetDateTimeNOW();
            }
        }

        public static DateTime? GetDateTimeNullable(object input)
        {
            try { return (DateTime.Parse(input.ToString())); }
            catch
            {
                return null;
            }
        }

        public static string GetDateTimeNullableString(object input)
        {
            try { return (DateTime.Parse(input.ToString()).ToString("dd.MM.yyyy")); }
            catch
            {
                return DateTime.Now.Date.ToString("dd.MM.yyyy");
            }
        }
        public static string GetDateTimeForFilter(object input)
        {
            try { return (DateTime.Parse(input.ToString()).ToString("yyyy-MM-dd")); }
            catch
            {
                return "";
            }
        }
        public static DateTime GetDateTimeNOW()
        {
            return (DateTime.UtcNow);
        }

        public static Decimal GetDecimal(object input)
        {
            try { return Decimal.Parse(input.ToString()); }
            catch { return 0; }
        }

        public static bool IsEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        public static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        public static T ParseEnum<T>(string value) where T:class
        {
            if (!string.IsNullOrEmpty(value))
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }

            return null;
        }
    }
}
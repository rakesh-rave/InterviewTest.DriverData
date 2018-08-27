using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace InterviewTest.DriverData
{
    public static class Common
    {
        /// <summary>
        /// Check Null and items exists in collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this IReadOnlyCollection<T> source, string message)
        {
            if (source == null || !source.Any())
            {
                throw new ArgumentNullException(nameof(source), message);
            }
            return true;
        }

        /// <summary>
        /// Read from Appsettings
        /// </summary>
        /// <param name="appsettingKey"></param>
        /// <returns></returns>

        public static string ReadConfig(this string appsettingKey)
        {
            return ConfigurationSettings.AppSettings[appsettingKey];
        }

        /// <summary>
        /// Convert string to TimeSpan
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>

        public static TimeSpan ToTimeSpan(this string source)
        {
            TimeSpan time;
            if (!TimeSpan.TryParse(source, out time))
            {
                throw new InvalidCastException(Constants.InvalidTimeFormatMessage);
            }
            return time;
        }

        /// <summary>
        /// Convert string to Decimal
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source)
        {
            decimal decimalvalue;
            if (!Decimal.TryParse(source, out decimalvalue))
            {
                throw new InvalidCastException(Constants.InvalidDecimalFormatMessage);
            }
            return decimalvalue;
        }

    }
    public enum RatingConfigSettings { StartTime, EndTime, MaxSpeed }
}

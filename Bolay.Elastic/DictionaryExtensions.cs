using Bolay.Elastic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic
{
    public static class DictionaryExtensions
    {
        #region String Methods
        /// <summary>
        /// Returns a string value from the dictionary. 
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>string</returns>
        public static string GetString(this Dictionary<string, object> dict, string key)
        {
            if (!dict.ContainsKey(key))
                throw new RequiredPropertyMissingException(key);

            return dict[key].ToString();
        }

        /// <summary>
        /// Returns a string value from the dictionary. 
        /// If the key does not exist the default value is returend.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>string</returns>
        public static string GetString(this Dictionary<string, object> dict, string key, string defaultValue)
        {
            if (!dict.ContainsKey(key))
                return defaultValue;

            return dict[key].ToString();
        }

        /// <summary>
        /// Returns a string value from the dictionary.
        /// If the key does not exist a default string value is returned.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns></returns>
        public static string GetStringOrDefault(this Dictionary<string, object> dict, string key)
        {
            if (dict == null || !dict.ContainsKey(key))
                return default(string);

            return dict[key].ToString(); 
        }
        #endregion

        #region Bool Methods
        /// <summary>
        /// Returns a bool value from a dictionary.
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// If the key does exist and the value is not an bool a ParsingException<bool> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>bool</returns>
        public static bool GetBool(this Dictionary<string, object> dict, string key)
        {
            string boolStr = dict.GetString(key);
            bool boolValue = false;
            if (!Boolean.TryParse(boolStr, out boolValue))
                throw new ParsingException<Boolean>(key);

            return boolValue;
        }

        /// <summary>
        /// Returns a bool value from a dictionary.
        /// If the key does not exist the default value will be returned.
        /// If the value is not an bool a ParsingException<bool> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <param name="defaultValue">The value to return as the default.</param>
        /// <returns>bool</returns>
        public static bool GetBool(this Dictionary<string, object> dict, string key, bool defaultValue)
        {
            try
            {
                return dict.GetBool(key);
            }
            catch (RequiredPropertyMissingException ex)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns a bool value from a dictionary.
        /// If the key does not exist the default Int32 value will be returned.
        /// If the value is not an bool a ParsingException<bool> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>bool</returns>
        public static bool GetBoolOrDefault(this Dictionary<string, object> dict, string key)
        {
            string boolStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(boolStr))
                return default(bool);

            bool boolValue = false;
            if (!Boolean.TryParse(boolStr, out boolValue))
                throw new ParsingException<bool>(key);

            return boolValue;
        }

        /// <summary>
        /// Returns a bool value from a dictionary.
        /// If the key does not exist a null value will be returned.
        /// If the value is not an bool a ParsingException<bool> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>bool?</returns>
        public static bool? GetBoolOrNull(this Dictionary<string, object> dict, string key)
        {
            string boolStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(boolStr))
                return null;

            bool boolValue = false;
            if (!Boolean.TryParse(boolStr, out boolValue))
                throw new ParsingException<bool>(key);

            return boolValue;
        }
        #endregion

        #region Int32 Methods
        /// <summary>
        /// Returns an Int32 value from a dictionary.
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// If the key does exist and the value is not an Int32 a ParsingException<Int32> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Int32</returns>
        public static Int32 GetInt32(this Dictionary<string, object> dict, string key)
        {
            string int32Str = dict.GetString(key);
            Int32 int32Value = 0;
            if (!Int32.TryParse(int32Str, out int32Value))
                throw new ParsingException<Int32>(key);

            return int32Value;
        }

        /// <summary>
        /// Returns an Int32 value from a dictionary.
        /// If the key does not exist the default value will be returned.
        /// If the value is not an Int32 a ParsingException<Int32> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <param name="defaultValue">The value to return as the default.</param>
        /// <returns>Int32</returns>
        public static Int32 GetInt32(this Dictionary<string, object> dict, string key, Int32 defaultValue)
        {
            try
            {
                return dict.GetInt32(key);
            }
            catch (RequiredPropertyMissingException ex)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns an Int32 value from a dictionary.
        /// If the key does not exist the default Int32 value will be returned.
        /// If the value is not an Int32 a ParsingException<Int32> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Int32</returns>
        public static Int32 GetInt32OrDefault(this Dictionary<string, object> dict, string key)
        {
            string int32Str = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(int32Str))
                return default(Int32);

            Int32 int32Value = 0;
            if (!Int32.TryParse(int32Str, out int32Value))
                throw new ParsingException<Int32>(key);

            return int32Value;
        }

        /// <summary>
        /// Returns an Int32 value from a dictionary.
        /// If the key does not exist a null value will be returned.
        /// If the value is not an Int32 a ParsingException<Int32> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Int32?</returns>
        public static Int32? GetInt32OrNull(this Dictionary<string, object> dict, string key)
        {
            string int32Str = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(int32Str))
                return null;

            Int32 int32Value = 0;
            if (!Int32.TryParse(int32Str, out int32Value))
                throw new ParsingException<Int32>(key);

            return int32Value;
        }
        #endregion

        #region Int64 Methods
        /// <summary>
        /// Returns an Int64 value from a dictionary.
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// If the key does exist and the value is not an Int64 a ParsingException<Int64> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Int64</returns>
        public static Int64 GetInt64(this Dictionary<string, object> dict, string key)
        {
            string int64Str = dict.GetString(key);
            Int64 int64Value = 0;
            if (!Int64.TryParse(int64Str, out int64Value))
                throw new ParsingException<Int64>(key);

            return int64Value;
        }

        /// <summary>
        /// Returns an Int64 value from a dictionary.
        /// If the key does not exist the default value will be returned.
        /// If the value is not an Int64 a ParsingException<Int64> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <param name="defaultValue">The value to return as the default.</param>
        /// <returns>Int64</returns>
        public static Int64 GetInt64(this Dictionary<string, object> dict, string key, Int64 defaultValue)
        {
            try
            {
                return dict.GetInt64(key);
            }
            catch (RequiredPropertyMissingException ex)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns an Int64 value from a dictionary.
        /// If the key does not exist the default Int64 value will be returned.
        /// If the value is not an Int64 a ParsingException<Int64> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Int64</returns>
        public static Int64 GetInt64OrDefault(this Dictionary<string, object> dict, string key)
        {
            string int64Str = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(int64Str))
                return default(Int64);

            Int64 int64Value = 0;
            if (!Int64.TryParse(int64Str, out int64Value))
                throw new ParsingException<Int64>(key);

            return int64Value;
        }

        /// <summary>
        /// Returns an Int64 value from a dictionary.
        /// If the key does not exist a null value will be returned.
        /// If the value is not an Int64 a ParsingException<Int64> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Int64?</returns>
        public static Int64? GetInt64OrNull(this Dictionary<string, object> dict, string key)
        {
            string int64Str = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(int64Str))
                return null;

            Int64 int64Value = 0;
            if (!Int64.TryParse(int64Str, out int64Value))
                throw new ParsingException<Int64>(key);

            return int64Value;
        }
        #endregion

        #region Double Methods
        /// <summary>
        /// Returns a Double value from a dictionary.
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// If the key does exist and the value is not an Double a ParsingException<Double> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Double</returns>
        public static Double GetDouble(this Dictionary<string, object> dict, string key)
        {
            string doubleStr = dict.GetString(key);
            Double doubleValue = default(Double);
            if (!Double.TryParse(doubleStr, out doubleValue))
                throw new ParsingException<Double>(key);

            return doubleValue;
        }

        /// <summary>
        /// Returns a Double value from a dictionary.
        /// If the key does not exist the default value will be returned.
        /// If the value is not an Double a ParsingException<Double> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <param name="defaultValue">The value to return as the default.</param>
        /// <returns>Double</returns>
        public static Double GetDouble(this Dictionary<string, object> dict, string key, Double defaultValue)
        {
            try
            {
                return dict.GetDouble(key);
            }
            catch (RequiredPropertyMissingException ex)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns a Double value from a dictionary.
        /// If the key does not exist the default Int64 value will be returned.
        /// If the value is not an Double a ParsingException<Double> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Double</returns>
        public static Double GetDoubleOrDefault(this Dictionary<string, object> dict, string key)
        {
            string int64Str = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(int64Str))
                return default(Int64);

            Int64 int64Value = 0;
            if (!Int64.TryParse(int64Str, out int64Value))
                throw new ParsingException<Int64>(key);

            return int64Value;
        }

        /// <summary>
        /// Returns a Double value from a dictionary.
        /// If the key does not exist a null value will be returned.
        /// If the value is not an Double a ParsingException<Double> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>Double?</returns>
        public static Double? GetDoubleOrNull(this Dictionary<string, object> dict, string key)
        {
            string doubleStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(doubleStr))
                return null;

            Double doubleValue = 0;
            if (!Double.TryParse(doubleStr, out doubleValue))
                throw new ParsingException<Double>(key);

            return doubleValue;
        }
        #endregion

        #region DateTime Methods
        /// <summary>
        /// Returns a DateTime value from a dictionary.
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// If the key does exist and the value is not an DateTime a ParsingException<DateTime> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>DateTime</returns>
        public static DateTime GetDateTime(this Dictionary<string, object> dict, string key)
        {
            string dateStr = dict.GetString(key);
            DateTime dateValue = new DateTime();
            if (!DateTime.TryParse(dateStr, out dateValue))
                throw new ParsingException<DateTime>(key);

            return dateValue;
        }

        /// <summary>
        /// Returns a DateTime value from a dictionary.
        /// If the key does not exist the default value will be returned.
        /// If the value is not an DateTime a ParsingException<DateTime> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <param name="defaultValue">The value to return as the default.</param>
        /// <returns>DateTime</returns>
        public static DateTime GetDateTime(this Dictionary<string, object> dict, string key, DateTime defaultValue)
        {
            try
            {
                return dict.GetDateTime(key);
            }
            catch (RequiredPropertyMissingException ex)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns a DateTime value from a dictionary.
        /// If the key does not exist the default Int64 value will be returned.
        /// If the value is not a DateTime a ParsingException<DateTime> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>DateTime</returns>
        public static DateTime GetDateTimeOrDefault(this Dictionary<string, object> dict, string key)
        {
            string dateStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(dateStr))
                return default(DateTime);

            DateTime dateValue = new DateTime();
            if (!DateTime.TryParse(dateStr, out dateValue))
                throw new ParsingException<DateTime>(key);

            return dateValue;
        }

        /// <summary>
        /// Returns a DateTime value from a dictionary.
        /// If the key does not exist a null value will be returned.
        /// If the value is not an Double a ParsingException<DateTime> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>DateTime?</returns>
        public static DateTime? GetDateTimeOrNull(this Dictionary<string, object> dict, string key)
        {
            string dateStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(dateStr))
                return null;

            DateTime datevalue = new DateTime();
            if (!DateTime.TryParse(dateStr, out datevalue))
                throw new ParsingException<DateTime>(key);

            return datevalue;
        }
        #endregion

        #region TimeSpan Methods

        /// <summary>
        /// Returns a TimeSpan value from a dictionary.
        /// If the key does not exist a RequiredPropertyMissingException is thrown.
        /// If the key does exist and the value is not an DateTime a ParsingException<TimeSpan> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>DateTime</returns>
        public static TimeSpan GetTimeSpan(this Dictionary<string, object> dict, string key)
        {
            string spanStr = dict.GetString(key);
            TimeSpan spanValue = new TimeSpan();
            if (!TimeSpan.TryParse(spanStr, out spanValue))
                throw new ParsingException<TimeSpan>(key);

            return spanValue;
        }

        /// <summary>
        /// Returns a TimeSpan value from a dictionary.
        /// If the key does not exist the default value will be returned.
        /// If the value is not an DateTime a ParsingException<TimeSpan> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <param name="defaultValue">The value to return as the default.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan GetTimeSpan(this Dictionary<string, object> dict, string key, TimeSpan defaultValue)
        {
            try
            {
                return dict.GetTimeSpan(key);
            }
            catch (RequiredPropertyMissingException ex)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns a TimeSpan value from a dictionary.
        /// If the key does not exist the default Int64 value will be returned.
        /// If the value is not a DateTime a ParsingException<TimeSpan> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan GetTimeSpanOrDefault(this Dictionary<string, object> dict, string key)
        {
            string spanStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(spanStr))
                return default(TimeSpan);

            TimeSpan spanValue = new TimeSpan();
            if (!TimeSpan.TryParse(spanStr, out spanValue))
                throw new ParsingException<TimeSpan>(key);

            return spanValue;
        }

        /// <summary>
        /// Returns a TimeSpan value from a dictionary.
        /// If the key does not exist a null value will be returned.
        /// If the value is not an Double a ParsingException<TimeSpan> will be thrown.
        /// </summary>
        /// <param name="dict">The dictionary to search in.</param>
        /// <param name="key">The key to search for in the dictionay.</param>
        /// <returns>TimeSpan?</returns>
        public static TimeSpan? GetTimeSpanOrNull(this Dictionary<string, object> dict, string key)
        {
            string spanStr = dict.GetStringOrDefault(key);
            if (string.IsNullOrWhiteSpace(spanStr))
                return null;

            TimeSpan spanValue = new TimeSpan();
            if (!TimeSpan.TryParse(spanStr, out spanValue))
                throw new ParsingException<TimeSpan>(key);

            return spanValue;
        }
        #endregion

        public static void AddObject<T>(this Dictionary<string, object> dict, string key, T value, T defaultValue = default(T))
        {
            if (dict == null)
                dict = new Dictionary<string, object>();

            if (value == null || (value is string && string.IsNullOrWhiteSpace(value as string)) || value.Equals(defaultValue))
            {
                return;
            }
                
            dict.Add(key, value);
        }
    }
}

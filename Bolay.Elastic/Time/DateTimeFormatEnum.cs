using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Time
{
    public sealed class DateTimeFormatEnum : TypeSafeEnumBase<DateTimeFormatEnum>
    {
        public string Format { get; set; }

        public static readonly DateTimeFormatEnum BasicDate = new DateTimeFormatEnum("basic_date", "yyyyMMdd");
        public static readonly DateTimeFormatEnum BasicDateTime = new DateTimeFormatEnum("basic_date_time", "yyyyMMdd’T'HHmmss.SSSZ");
        public static readonly DateTimeFormatEnum BasicDateTimeNoMilliseconds = new DateTimeFormatEnum("basic_date_time_no_millis", "yyyyMMdd’T'HHmmssZ");
        public static readonly DateTimeFormatEnum BasicOrdinalDate = new DateTimeFormatEnum("basic_ordinal_date", "yyyyDDD");
        public static readonly DateTimeFormatEnum BasicOrdinalDateTime = new DateTimeFormatEnum("basic_ordinal_date_time", "yyyyDDD’T'HHmmss.SSSZ");
        public static readonly DateTimeFormatEnum BasicOrdinalDateTimeNoMilliseconds = new DateTimeFormatEnum("basic_ordinal_date_time_no_millis", "yyyyDDD’T'HHmmssZ");
        public static readonly DateTimeFormatEnum BasicTime = new DateTimeFormatEnum("basic_time", "HHmmss.SSSZ");
        public static readonly DateTimeFormatEnum BasicTimeNoMilliseconds = new DateTimeFormatEnum("basic_time_no_millis", "HHmmssZ");
        public static readonly DateTimeFormatEnum BasicTTime = new DateTimeFormatEnum("basic_t_time", "'T’HHmmss.SSSZ");
        public static readonly DateTimeFormatEnum BasicTTimeNoMilliseconds = new DateTimeFormatEnum("basic_t_time_no_millis", "'T’HHmmssZ");
        public static readonly DateTimeFormatEnum BasicWeekDate = new DateTimeFormatEnum("basic_week_date", "xxxx’W'wwe");
        public static readonly DateTimeFormatEnum BasicWeekDateTime = new DateTimeFormatEnum("basic_week_date_time", "xxxx’W'wwe’T'HHmmss.SSSZ");
        public static readonly DateTimeFormatEnum BasicWeekDateTimeNoMilliseconds = new DateTimeFormatEnum("basic_week_date_time_no_millis", "xxxx’W'wwe’T'HHmmssZ");
        public static readonly DateTimeFormatEnum Date = new DateTimeFormatEnum("date", "yyyy-MM-dd");
        public static readonly DateTimeFormatEnum DateHour = new DateTimeFormatEnum("date_hour", "yyyy-MM-dd’T'HH");
        public static readonly DateTimeFormatEnum DateHourMinute = new DateTimeFormatEnum("date_hour_minute", "yyyy-MM-dd’T'HH:mm");
        public static readonly DateTimeFormatEnum DateHourMinuteSecond = new DateTimeFormatEnum("date_hour_minute_second", "yyyy-MM-dd’T'HH:mm:ss");
        public static readonly DateTimeFormatEnum DateHourMinuteSecondFraction = new DateTimeFormatEnum("date_hour_minute_second_fraction", "yyyy-MM-dd’T'HH:mm:ss.SSS");
        public static readonly DateTimeFormatEnum DateHourMinuteSecondMilliseconds = new DateTimeFormatEnum("date_hour_minute_second_millis", "yyyy-MM-dd’T'HH:mm:ss.SSS");
        public static readonly DateTimeFormatEnum DateOptionalTime = new DateTimeFormatEnum("date_optional_time", "yyyy-MM-dd HH:mm:ss.SSS");
        public static readonly DateTimeFormatEnum DateTime = new DateTimeFormatEnum("date_time", "yyyy-MM-dd’T'HH:mm:ss.SSSZZ");
        public static readonly DateTimeFormatEnum DateTimeNoMilliseconds = new DateTimeFormatEnum("date_time_no_millis", "yyyy-MM-dd’T'HH:mm:ssZZ");
        public static readonly DateTimeFormatEnum Hour = new DateTimeFormatEnum("hour", "HH");
        public static readonly DateTimeFormatEnum HourMinute = new DateTimeFormatEnum("hour_minute", "HH:mm");
        public static readonly DateTimeFormatEnum HourMinuteSecond = new DateTimeFormatEnum("hour_minute_second", "HH:mm:ss");
        public static readonly DateTimeFormatEnum HourMinuteSecondFraction = new DateTimeFormatEnum("hour_minute_second_fraction", "HH:mm:ss.SSS");
        public static readonly DateTimeFormatEnum HourMinuteSecondMilliseconds = new DateTimeFormatEnum("hour_minute_second_millis", "HH:mm:ss.SSS");
        public static readonly DateTimeFormatEnum OrdinalDate = new DateTimeFormatEnum("ordinal_date", "yyyy-DDD");
        public static readonly DateTimeFormatEnum OrdinalDateTime = new DateTimeFormatEnum("ordinal_date_time", "yyyy-DDD’T'HH:mm:ss.SSSZZ");
        public static readonly DateTimeFormatEnum OrdinalDateTimeNoMilliseconds = new DateTimeFormatEnum("ordinal_date_time_no_millis", "yyyy-DDD’T'HH:mm:ssZZ");
        public static readonly DateTimeFormatEnum Time = new DateTimeFormatEnum("time", "HH:mm:ss.SSSZZ");
        public static readonly DateTimeFormatEnum TimeNoMilliseconds = new DateTimeFormatEnum("time_no_millis", "HH:mm:ssZZ");
        public static readonly DateTimeFormatEnum TTime = new DateTimeFormatEnum("t_time", "'T’HH:mm:ss.SSSZZ");
        public static readonly DateTimeFormatEnum TTimeNoMilliseconds = new DateTimeFormatEnum("t_time_no_millis", "'T’HH:mm:ssZZ");
        public static readonly DateTimeFormatEnum WeekDate = new DateTimeFormatEnum("week_date", "xxxx-'W’ww-e");
        public static readonly DateTimeFormatEnum WeekDateTime = new DateTimeFormatEnum("week_date_time", "xxxx-'W’ww-e’T'HH:mm:ss.SSSZZ");
        public static readonly DateTimeFormatEnum WeekDateTimeNoMilliseconds = new DateTimeFormatEnum("weekDateTimeNoMillis", "xxxx-'W’ww-e’T'HH:mm:ssZZ");
        public static readonly DateTimeFormatEnum WeekYear = new DateTimeFormatEnum("week_year", "xxxx");
        public static readonly DateTimeFormatEnum WeekYearWeek = new DateTimeFormatEnum("week_year_week", "xxxx-'W’ww");
        public static readonly DateTimeFormatEnum WeekYearWeekDay = new DateTimeFormatEnum("week_year_week_day", "xxxx-'W’ww-e");
        public static readonly DateTimeFormatEnum Year = new DateTimeFormatEnum("year", "yyyy");
        public static readonly DateTimeFormatEnum YearMonth = new DateTimeFormatEnum("year_month", "yyyy-MM");
        public static readonly DateTimeFormatEnum YearMonthDay = new DateTimeFormatEnum("year_month_day", "yyyy-MM-dd");

        private DateTimeFormatEnum(string name, string format)
            : base(name)
        {
            Format = format;
            _AllItems.Add(this);
        }
    }
}

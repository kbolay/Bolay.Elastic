using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Time
{
    [JsonConverter(typeof(TimeSerializer))]
    public class TimeValue
    {
        public Int64 Size { get; private set; }
        public TimeUnit Unit { get; private set; }

        public TimeSpan TimeSpan 
        { 
            get 
            {
                Int64 totalMilliseconds = Size * Unit.MillisecondsPerUnit;
                return new TimeSpan(0, 0, 0, 0, (int)totalMilliseconds);
            } 
        }

        public TimeValue(string value)
        {
            value = value.Trim();
            if (value.EndsWith(TimeUnit.Millisecond.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(TimeUnit.Millisecond.ToString(), ""));
                Unit = TimeUnit.Millisecond;                
            }
            else if (value.EndsWith(TimeUnit.Minute.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(TimeUnit.Minute.ToString(), ""));
                Unit = TimeUnit.Minute; 
            }
            else if (value.EndsWith(TimeUnit.Hours.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(TimeUnit.Hours.ToString(), ""));
                Unit = TimeUnit.Hours;
            }
            else if (value.EndsWith(TimeUnit.Days.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(TimeUnit.Days.ToString(), ""));
                Unit = TimeUnit.Days;
            }
            else if (value.EndsWith(TimeUnit.Weeks.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(TimeUnit.Weeks.ToString(), ""));
                Unit = TimeUnit.Weeks;
            }
            else
            {
                Int64 sizeValue;
                if (Int64.TryParse(value, out sizeValue))
                {
                    Size = sizeValue;
                    Unit = TimeUnit.Millisecond;
                }
                else
                    throw new ArgumentException("String cannot be converted into expiration time.");
            }
        }
        public TimeValue(TimeSpan timeSpan)
        {
            if (timeSpan == default(TimeSpan))
                throw new ArgumentNullException("timeSpan", "This constructor for TimeValue requires a valid TimeSpan.");

            Size = (Int64)timeSpan.TotalMilliseconds;
            Unit = TimeUnit.Millisecond;

        }
        public TimeValue(Int64 size, TimeUnit unit)
        {

            Size = size;

            if (unit != null)
                Unit = unit;
            else
                Unit = TimeUnit.Millisecond;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Size, Unit);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TimeValue))
                return false;

            if (obj == null)
                return false;

            TimeValue timeValue = obj as TimeValue;
            if (this.TimeSpan.Equals(timeValue.TimeSpan))
            {
                return true;
            }

            return false;
        }
    }
}

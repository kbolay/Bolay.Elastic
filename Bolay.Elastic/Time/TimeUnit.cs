using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Time
{
    public sealed class TimeUnit : TypeSafeEnumBase<TimeUnit>
    {
        public Int64 MillisecondsPerUnit { get; private set; }

        public static readonly TimeUnit Millisecond = new TimeUnit("ms", 1);
        public static readonly TimeUnit Minute = new TimeUnit("m", 60000);
        public static readonly TimeUnit Hours = new TimeUnit("h", 3600000);
        public static readonly TimeUnit Days = new TimeUnit("d", 86400000);
        public static readonly TimeUnit Weeks = new TimeUnit("w", 604800000);

        private TimeUnit(string value, Int64 milliseconds) : base(value)
        {
            MillisecondsPerUnit = milliseconds;
            _AllItems.Add(this);
        }
    }
}

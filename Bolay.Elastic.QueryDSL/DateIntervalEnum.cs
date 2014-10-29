using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public sealed class DateIntervalEnum : TypeSafeEnumBase<DateIntervalEnum>
    {
        public static readonly DateIntervalEnum Minute = new DateIntervalEnum("minute");
        public static readonly DateIntervalEnum Hour = new DateIntervalEnum("hour");
        public static readonly DateIntervalEnum Day = new DateIntervalEnum("day");
        public static readonly DateIntervalEnum Week = new DateIntervalEnum("week");
        public static readonly DateIntervalEnum Month = new DateIntervalEnum("month");
        public static readonly DateIntervalEnum Quarter = new DateIntervalEnum("quarter");
        public static readonly DateIntervalEnum Year = new DateIntervalEnum("year");

        private DateIntervalEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}

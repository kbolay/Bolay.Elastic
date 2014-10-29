using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Aggregations.Average;
using Bolay.Elastic.QueryDSL.Aggregations.ValueCount;
using Bolay.Elastic.QueryDSL.Aggregations.Filter;
using Bolay.Elastic.QueryDSL.Aggregations.GeoDistance;
using Bolay.Elastic.QueryDSL.Aggregations.GeoHashGrid;
using Bolay.Elastic.QueryDSL.Aggregations.Global;
using Bolay.Elastic.QueryDSL.Aggregations.Histogram;
using Bolay.Elastic.QueryDSL.Aggregations.Histogram.Date;
using Bolay.Elastic.QueryDSL.Aggregations.Maximum;
using Bolay.Elastic.QueryDSL.Aggregations.Minimum;
using Bolay.Elastic.QueryDSL.Aggregations.Nested;
using Bolay.Elastic.QueryDSL.Aggregations.Range;
using Bolay.Elastic.QueryDSL.Aggregations.Range.Date;
using Bolay.Elastic.QueryDSL.Aggregations.Range.IPv4;
using Bolay.Elastic.QueryDSL.Aggregations.Statistics;
using Bolay.Elastic.QueryDSL.Aggregations.Statistics.Extended;
using Bolay.Elastic.QueryDSL.Aggregations.Sum;
using Bolay.Elastic.QueryDSL.Aggregations.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bolay.Elastic.QueryDSL.Aggregations.Missing;
using Bolay.Elastic.QueryDSL.Aggregations.Percentiles;

namespace Bolay.Elastic.QueryDSL.Aggregations
{
    public sealed class AggregationTypeEnum : TypeSafeEnumBase<AggregationTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly AggregationTypeEnum Average = new AggregationTypeEnum("avg", typeof(AverageAggregate));        
        public static readonly AggregationTypeEnum Filter = new AggregationTypeEnum("filter", typeof(FilterAggregate));
        public static readonly AggregationTypeEnum GeoDistance = new AggregationTypeEnum("geo_distance", typeof(GeoDistanceAggregate));
        public static readonly AggregationTypeEnum GeoHashGrid = new AggregationTypeEnum("geohash_grid", typeof(GeoHashGridAggregate));
        public static readonly AggregationTypeEnum Global = new AggregationTypeEnum("global", typeof(GlobalAggregate));
        public static readonly AggregationTypeEnum Histogram = new AggregationTypeEnum("histogram", typeof(HistogramAggregate));
        public static readonly AggregationTypeEnum DateHistogram = new AggregationTypeEnum("date_histogram", typeof(DateHistogramAggregate));
        public static readonly AggregationTypeEnum Maximum = new AggregationTypeEnum("max", typeof(MaximumAggregate));
        public static readonly AggregationTypeEnum Minimum = new AggregationTypeEnum("min", typeof(MinimumAggregate));
        public static readonly AggregationTypeEnum Missing = new AggregationTypeEnum("missing", typeof(MissingAggregate));
        public static readonly AggregationTypeEnum Nested = new AggregationTypeEnum("nested", typeof(NestedAggregate));
        public static readonly AggregationTypeEnum Range = new AggregationTypeEnum("range", typeof(RangeAggregate));
        public static readonly AggregationTypeEnum DateRange = new AggregationTypeEnum("date_range", typeof(DateRangeAggregate));
        public static readonly AggregationTypeEnum IpRange = new AggregationTypeEnum("ip_range", typeof(IPv4RangeAggregate));
        public static readonly AggregationTypeEnum Statistics = new AggregationTypeEnum("stats", typeof(StatisticsAggregate));
        public static readonly AggregationTypeEnum ExtendedStatistics = new AggregationTypeEnum("extended_stats", typeof(ExtendedStatisticsAggregate));
        public static readonly AggregationTypeEnum Sum = new AggregationTypeEnum("sum", typeof(SumAggregate));
        public static readonly AggregationTypeEnum Terms = new AggregationTypeEnum("terms", typeof(TermsAggregate));
        public static readonly AggregationTypeEnum ValueCount = new AggregationTypeEnum("value_count", typeof(ValueCountAggregate));
        public static readonly AggregationTypeEnum Percentiles = new AggregationTypeEnum("percentiles", typeof(PercentilesAggregate));

        private AggregationTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}

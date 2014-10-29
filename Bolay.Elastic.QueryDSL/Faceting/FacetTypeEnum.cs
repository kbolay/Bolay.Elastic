using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Faceting.DateHistogram;
using Bolay.Elastic.QueryDSL.Faceting.Filter;
using Bolay.Elastic.QueryDSL.Faceting.GeoDistance;
using Bolay.Elastic.QueryDSL.Faceting.Histogram;
using Bolay.Elastic.QueryDSL.Faceting.Query;
using Bolay.Elastic.QueryDSL.Faceting.Range;
using Bolay.Elastic.QueryDSL.Faceting.Statistics;
using Bolay.Elastic.QueryDSL.Faceting.Terms;
using Bolay.Elastic.QueryDSL.Faceting.TermsStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    public sealed class FacetTypeEnum : TypeSafeEnumBase<FacetTypeEnum>
    {
        public Type ImplementationType { get; private set; }
        public Type ResponseType { get; private set; }
        public static readonly FacetTypeEnum Terms = new FacetTypeEnum("terms", typeof(TermsFacet), typeof(TermsResponse));
        public static readonly FacetTypeEnum Range = new FacetTypeEnum("range", typeof(RangeFacet), typeof(RangeResponse));
        public static readonly FacetTypeEnum Histogram = new FacetTypeEnum("histogram", typeof(HistogramFacet), typeof(HistogramResponse));
        public static readonly FacetTypeEnum DateHistogram = new FacetTypeEnum("date_histogram", typeof(DateHistogramFacet), typeof(DateHistogramResponse));
        public static readonly FacetTypeEnum Filter = new FacetTypeEnum("filter", typeof(FilterFacet), typeof(FilterResponse));
        public static readonly FacetTypeEnum Query = new FacetTypeEnum("query", typeof(QueryFacet), typeof(QueryResponse));
        public static readonly FacetTypeEnum Statistical = new FacetTypeEnum("statistical", typeof(StatisticsFacet), typeof(StatisticsResponse));
        public static readonly FacetTypeEnum GeoDistance = new FacetTypeEnum("geo_distance", typeof(GeoDistanceFacet), typeof(GeoDistanceResponse));
        public static readonly FacetTypeEnum TermsStatistics = new FacetTypeEnum("terms_stats", typeof(TermsStatisticsFacet), typeof(TermsStatisticsResponse));

        private FacetTypeEnum(string value, Type implementationType, Type responseType)
            : base(value)
        {
            ImplementationType = implementationType;
            ResponseType = responseType;
            _AllItems.Add(this);
        }
    }
}

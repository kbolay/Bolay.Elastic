using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Filters.And;
using Bolay.Elastic.QueryDSL.Filters.Bool;
using Bolay.Elastic.QueryDSL.Filters.DocumentType;
using Bolay.Elastic.QueryDSL.Filters.Exists;
using Bolay.Elastic.QueryDSL.Filters.GeoBoundingBox;
using Bolay.Elastic.QueryDSL.Filters.GeoDistance;
using Bolay.Elastic.QueryDSL.Filters.GeoDistanceRange;
using Bolay.Elastic.QueryDSL.Filters.GeoHashCell;
using Bolay.Elastic.QueryDSL.Filters.GeoPolygon;
using Bolay.Elastic.QueryDSL.Filters.HasChild;
using Bolay.Elastic.QueryDSL.Filters.HasParent;
using Bolay.Elastic.QueryDSL.Filters.Ids;
using Bolay.Elastic.QueryDSL.Filters.Indices;
using Bolay.Elastic.QueryDSL.Filters.Limit;
using Bolay.Elastic.QueryDSL.Filters.MatchAll;
using Bolay.Elastic.QueryDSL.Filters.Missing;
using Bolay.Elastic.QueryDSL.Filters.Nested;
using Bolay.Elastic.QueryDSL.Filters.Not;
using Bolay.Elastic.QueryDSL.Filters.Or;
using Bolay.Elastic.QueryDSL.Filters.Prefix;
using Bolay.Elastic.QueryDSL.Filters.Query;
using Bolay.Elastic.QueryDSL.Filters.Range;
using Bolay.Elastic.QueryDSL.Filters.Regex;
using Bolay.Elastic.QueryDSL.Filters.Script;
using Bolay.Elastic.QueryDSL.Filters.Term;
using Bolay.Elastic.QueryDSL.Filters.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters
{
    public sealed class FilterTypeEnum : TypeSafeEnumBase<FilterTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly FilterTypeEnum Term = new FilterTypeEnum("term", typeof(TermFilter));
        public static readonly FilterTypeEnum And = new FilterTypeEnum("and", typeof(AndFilter));
        public static readonly FilterTypeEnum Bool = new FilterTypeEnum("bool", typeof(BoolFilter));
        public static readonly FilterTypeEnum Exists = new FilterTypeEnum("exists", typeof(ExistsFilter));
        public static readonly FilterTypeEnum GeoBoundingBox = new FilterTypeEnum("geo_bounding_box", typeof(GeoBoundingBoxFilter));
        public static readonly FilterTypeEnum GeoDistance = new FilterTypeEnum("geo_distance", typeof(GeoDistanceFilter));
        public static readonly FilterTypeEnum GeoDistanceRange = new FilterTypeEnum("geo_distance_range", typeof(GeoDistanceRangeFilter));
        public static readonly FilterTypeEnum GeoPolygon = new FilterTypeEnum("geo_polygon", typeof(GeoPolygonFilter));
        public static readonly FilterTypeEnum GeoHashCell = new FilterTypeEnum("geohash_cell", typeof(GeoHashCellFilter));
        public static readonly FilterTypeEnum HasChild = new FilterTypeEnum("has_child", typeof(HasChildFilter));
        public static readonly FilterTypeEnum HasParent = new FilterTypeEnum("has_parent", typeof(HasParentFilter));
        public static readonly FilterTypeEnum Ids = new FilterTypeEnum("ids", typeof(IdsFilter));
        public static readonly FilterTypeEnum Indices = new FilterTypeEnum("indices", typeof(IndicesFilter));
        public static readonly FilterTypeEnum Limit = new FilterTypeEnum("limit", typeof(LimitFilter));
        public static readonly FilterTypeEnum MatchAll = new FilterTypeEnum("match_all", typeof(MatchAllFilter));
        public static readonly FilterTypeEnum Missing = new FilterTypeEnum("missing", typeof(MissingFilter));
        public static readonly FilterTypeEnum Nested = new FilterTypeEnum("nested", typeof(NestedFilter));
        public static readonly FilterTypeEnum Not = new FilterTypeEnum("not", typeof(NotFilter));
        public static readonly FilterTypeEnum Or = new FilterTypeEnum("or", typeof(OrFilter));
        public static readonly FilterTypeEnum Prefix = new FilterTypeEnum("prefix", typeof(PrefixFilter));
        public static readonly FilterTypeEnum Query = new FilterTypeEnum("query", typeof(QueryFilter));
        public static readonly FilterTypeEnum FQuery = new FilterTypeEnum("fquery", typeof(QueryFilter));
        public static readonly FilterTypeEnum Range = new FilterTypeEnum("range", typeof(RangeFilterBase));
        public static readonly FilterTypeEnum Regex = new FilterTypeEnum("regexp", typeof(RegexFilter));
        public static readonly FilterTypeEnum Script = new FilterTypeEnum("script", typeof(ScriptFilter));
        public static readonly FilterTypeEnum Terms = new FilterTypeEnum("terms", typeof(TermsFilter));
        public static readonly FilterTypeEnum In = new FilterTypeEnum("in", typeof(TermsFilter));
        public static readonly FilterTypeEnum Type = new FilterTypeEnum("type", typeof(TypeFilter));

        private FilterTypeEnum(string value, Type implementationType)
            : base(value)
        {
            if (implementationType == null)
                throw new ArgumentNullException("implementationType", "FilterTypeEnum requires an implementation type.");

            ImplementationType = implementationType;
            _AllItems.Add(this);
        }
    }
}

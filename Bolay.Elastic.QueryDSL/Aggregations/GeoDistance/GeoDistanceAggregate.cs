using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.GeoDistance
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-geodistance-aggregation.html
    /// </summary>
    [JsonConverter(typeof(GeoDistanceSerializer))]
    public class GeoDistanceAggregate : BucketAggregationBase
    {
        /// <summary>
        /// Gets the geo_point field to calculate distance with.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the point of origin to calculate distance from.
        /// </summary>
        public CoordinatePoint OriginPoint { get; private set; }

        /// <summary>
        /// Gets or sets the unit of distance to use.
        /// Defaults to km.
        /// </summary>
        public DistanceUnitEnum Unit { get; set; }

        /// <summary>
        /// Gets or sets the method used for computing distance.
        /// Defaults to sloppy_arc.
        /// </summary>
        public DistanceComputeTypeEnum DistanceComputeType { get; set; }

        /// <summary>
        /// Gets the ranges for the various buckets.
        /// </summary>
        public IEnumerable<DistanceRangeBucket> Ranges { get; private set; }

        /// <summary>
        /// Creates a geo distance aggregation.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the geo_point field to calculate distances with.</param>
        /// <param name="originPoint">Sets the origin point to calculate distances from.</param>
        public GeoDistanceAggregate(string name, string field, CoordinatePoint originPoint, IEnumerable<DistanceRangeBucket> ranges)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoDistanceAggregate requires a field.");
            if (originPoint == null)
                throw new ArgumentNullException("field", "GeoDistanceAggregate requires an origin point.");
            if (ranges == null || ranges.All(x => x == null))
                throw new ArgumentNullException("ranges", "GeoDistanceAggregate requires at least one range bucket.");

            Field = field;
            OriginPoint = originPoint;
            Ranges = ranges.Where(x => x != null);

            Unit = GeoDistanceSerializer._UNIT_DEFAULT;
            DistanceComputeType = GeoDistanceSerializer._DISTANCE_TYPE_DEFAULT;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.GeoHashGrid
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-geohashgrid-aggregation.html
    /// </summary>
    [JsonConverter(typeof(GeoHashGridSerializer))]
    public class GeoHashGridAggregate : BucketAggregationBase
    {
        private int _Size { get; set; }
        private int _ShardSize { get; set; }

        /// <summary>
        /// Gets the field.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the precision. Value can be from 1 to 12.
        /// Defaults to 5.
        /// </summary>
        public int Precision { get; private set; }

        /// <summary>
        /// Gets or sets the maximum number of geohash buckets to return. A value of 0 will return all buckets that contian a hit.
        /// Defaults to 10,000.
        /// </summary>
        public int Size
        {
            get { return _Size; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Size", "Size must be greater than or equal to zero.");

                _Size = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of buckets to return from each shard. A value of 0 will be considered unlimited.
        /// Defaults to size.
        /// </summary>
        public int ShardSize
        {
            get { return _ShardSize; }
            set 
            {
                if (value < Size)
                    throw new ArgumentOutOfRangeException("ShardSize", "ShardSize must be greater than or equal to Size.");

                _ShardSize = value;
            }
        }

        /// <summary>
        /// Create a geohash_grid using the default precision.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the geo_point field to use.</param>
        public GeoHashGridAggregate(string name, string field)
            : base(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoHashGridAggregate requires a feild.");

            Field = field;
            Precision = GeoHashGridSerializer._PRECISION_DEFAULT;
            Size = GeoHashGridSerializer._SIZE_DEFAULT;
            ShardSize = GeoHashGridSerializer._SIZE_DEFAULT;
        }

        /// <summary>
        /// Creates geohash_grid aggregate.
        /// </summary>
        /// <param name="name">Sets the aggregation name.</param>
        /// <param name="field">Sets the geo_point field.</param>
        /// <param name="precision">Sets the precision value.</param>
        public GeoHashGridAggregate(string name, string field, int precision)
            : this(name, field)
        {
            if (precision < 1 || precision > 12)
                throw new ArgumentOutOfRangeException("precision", "Precision must be from 1 to 12.");
            Precision = precision;
        }
    }
}

using Bolay.Elastic.Distance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.GeoShape
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-geo-shape-type.html
    /// </summary>
    [JsonConverter(typeof(GeoShapePropertySerializer))]
    public class GeoShapeProperty : DocumentPropertyBase
    {
        private const Double _MAXIMUM_DISTANCE_ERROR_PERCENTAGE = 0.5;

        internal static PrefixTreeEnum _TREE_DEFAULT = PrefixTreeEnum.GeoHash;
        internal const Double _DISTANCE_ERROR_PERCENTAGE_DEFAULT = 0.025;

        private Double _DistanceErrorPercentage { get; set; }

        ///<summary>
        /// The PrefixTree implementation to be used.
        /// Defaults to geohash.
        ///</summary>
        public PrefixTreeEnum Tree { get; set; }

        /// <summary>
        /// This parameter may be used instead of TreeLevels to set an appropriate value for the TreeLevels parameter.
        /// The value specifies the desired precision and Elasticsearch will calculate the best TreeLevels value to honor this precision. 
        /// Default unit is meters.
        /// </summary>
        public DistanceValue Precision { get; set; }

        /// <summary>
        /// Maximum number of layers to be used by the PrefixTree. This can be used to control the precision of 
        /// shape representations and therefore how many terms are indexed. Defaults to the default value of the 
        /// chosen PrefixTree implementation. Since this parameter requires a certain level of understanding of the 
        /// underlying implementation, users may use the precision parameter instead. However, Elasticsearch only 
        /// uses the tree_levels parameter internally and this is what is returned via the mapping API even if you 
        /// use the precision parameter.
        /// </summary>
        public Int64? TreeLevels { get; set; }

        /// <summary>
        /// Used as a hint to the PrefixTree about how precise it should be. 
        /// Defaults to 0.025 (2.5%) with 0.5 as the maximum supported value.
        /// </summary>
        public Double DistanceErrorPercentage
        {
            get { return _DistanceErrorPercentage; }
            set
            {
                if (value < 0 || value > 0.5)
                {
                    throw new ArgumentOutOfRangeException("DistanceErrorPercentage", "DistanceErrorPercentage must be greater than or eqaul to 0.0 and less than 0.5.");
                }

                _DistanceErrorPercentage = value;
            }
        }

        /// <summary>
        /// Establish defaults.
        /// </summary>
        public GeoShapeProperty(string name) : base(name, PropertyTypeEnum.GeoShape)
        {
            Tree = _TREE_DEFAULT;
            DistanceErrorPercentage = _DISTANCE_ERROR_PERCENTAGE_DEFAULT;
        }
    }
}

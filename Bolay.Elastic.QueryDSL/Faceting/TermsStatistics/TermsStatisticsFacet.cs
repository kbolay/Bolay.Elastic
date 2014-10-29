using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.TermsStatistics
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-terms-stats-facet.html
    /// </summary>
    [JsonConverter(typeof(TermsStatisticsSerializer))]
    public class TermsStatisticsFacet : FacetBase
    {
        internal static OrderOptionEnum _ORDER_DEFAULT = OrderOptionEnum.Count;
        internal const int _SIZE_DEFAULT = 5;

        private int _Size { get; set; }
        private int _ShardSize { get; set; }

        /// <summary>
        /// Gets the key_field value.
        /// </summary>
        public string KeyField { get; private set; }

        /// <summary>
        /// Gets the value_field value.
        /// </summary>
        public string ValueField { get; private set; }

        /// <summary>
        /// Gets the value_script value.
        /// </summary>
        public Script ValueScript { get; private set; }

        /// <summary>
        /// Gets or sets the shard_size.
        /// Defaults to size.
        /// </summary>
        public int ShardSize
        {
            get { return _ShardSize; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("ShardSize", "ShardSize must be greater than or equal to zero.");

                _ShardSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to sort terms by.
        /// Defaults to count.
        /// </summary>
        public OrderOptionEnum Order { get; set; }

        private TermsStatisticsFacet(string facetName, string keyField)
            : base(facetName)
        {
            if (string.IsNullOrWhiteSpace(keyField))
                throw new ArgumentNullException("field", "TermsStatisticsFacet requires a field.");

            KeyField = keyField;
            Size = _SIZE_DEFAULT;
            ShardSize = _SIZE_DEFAULT;
            Order = _ORDER_DEFAULT;
        }

        /// <summary>
        /// Create a terms_stats facet based on key_field and value_field.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet request.</param>
        /// <param name="keyField">Sets the key_field value.</param>
        /// <param name="valueField">Sets the value_field value.</param>
        public TermsStatisticsFacet(string facetName, string keyField, string valueField)
            : this(facetName, keyField)
        {
            if (string.IsNullOrWhiteSpace(valueField))
                throw new ArgumentNullException("valueField", "TermsStatisticsFacet requires a value field in this constructor.");

            ValueField = valueField;
        }

        /// <summary>
        /// Creates the terms_stats facet request based on key_field and value_script.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet request.</param>
        /// <param name="keyField">Sets the key_field value.</param>
        /// <param name="valueScript">Sets the value_script value.</param>
        public TermsStatisticsFacet(string facetName, string keyField, Script valueScript)
            : this(facetName, keyField)
        {
            if (valueScript == null)
                throw new ArgumentNullException("valueScript", "TermsStatisticsFacet requires a value script in this constructor.");

            ValueScript = valueScript;
        }
    }
}

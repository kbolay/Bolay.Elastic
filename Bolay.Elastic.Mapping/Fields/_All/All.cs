using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._All
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-all-field.html
    /// </summary>
    [JsonConverter(typeof(AllSerializer))]
    public class All
    {
        internal const bool _IS_ENABLED_DEFAULT = true;
        internal static bool _STORE_DEFAULT = false;
        internal static TermVectorEnum _TERM_VECTOR_DEFAULT = TermVectorEnum.No;

        /// <summary>
        /// Gets or sets the enabled value.
        /// Defaults to true;
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the store value.
        /// Defaults to false.
        /// </summary>
        public bool Store { get; set; }

        /// <summary>
        /// Gets or sets the term_vector.
        /// Defaults to no.
        /// </summary>
        public TermVectorEnum TermVector { get; set; }

        /// <summary>
        /// Gets or sets the field to include in _all.
        /// </summary>
        public IEnumerable<string> Includes { get; set; }

        /// <summary>
        /// Gets or sets the field to exclude from _all.
        /// </summary>
        public IEnumerable<string> Excludes { get; set; }

        /// <summary>
        /// Gets or sets the analysis to perform for the _all properties.
        /// </summary>
        public PropertyAnalyzer Analyzer { get; set; }

        public All()
        {
            IsEnabled = _IS_ENABLED_DEFAULT;
            Store = _STORE_DEFAULT;
            TermVector = _TERM_VECTOR_DEFAULT;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is All))
                return false;

            All all = obj as All;
            if (all == null)
                return false;

            if (this.IsEnabled == all.IsEnabled && this.Store == all.Store && this.TermVector == all.TermVector
                && this.Includes == all.Includes && this.Excludes == all.Excludes && this.Analyzer.Equals(all.Analyzer))
            {
                return true;
            }                

            return false;
        }
    }
}

using Bolay.Elastic.Api.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.String
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-core-types.html#string
    /// </summary>
    [JsonConverter(typeof(StringPropertySerializer))]
    public class StringProperty : FieldProperty
    {
        // TODO: figure out a good way to force the specific defaults in the case of a change on index value
        // i think this has to be done in the constructor...
        internal static TermVectorEnum _TERM_VECTOR_DEFAULT = TermVectorEnum.No;
        internal static IndexOptionEnum _INDEX_OPTION_ANALYZED_DEFAULT = IndexOptionEnum.Position;
        internal static IndexOptionEnum _INDEX_OPTION_NOT_ANALYZED_DEFAULT = IndexOptionEnum.DocumentId;
        internal const bool _OMIT_NORMS_ANALYZED_DEFAULT = false;
        internal const bool _OMIT_NORMS_NOT_ANALYZED_DEFAULT = true;
        internal const Int64 _POSITION_OFFSET_GAP_DEFAULT = 0;
        internal const bool _DOC_VALUES_DEFAULT = false;
        internal const int _PRECISION_STEP_DEFAULT = 4;

        private Int64? _IgnoreAbove { get; set; }
        private string _NullValue { get; set; }

        /// <summary>
        /// Gets or sets the doc_values.
        /// Defaults to false, unless TermVector is set to doc_values.
        /// </summary>
        public bool DocValues { get; set; }

        /// <summary>
        /// Gets or sets the norms values.
        /// </summary>
        public Norms Norms { get; set; }

        /// <summary>
        /// The value to insert into the index when the field is null.
        /// The default is to not add anything to the index.
        /// </summary>
        public override object NullValue { get { return _NullValue; } set { _NullValue = value.ToString(); } }

        /// <summary>
        /// http://blog.jpountz.net/post/41301889664/putting-term-vectors-on-a-diet
        /// Defaults to no.
        /// </summary>
        public TermVectorEnum TermVector { get; set; }

        /// <summary>
        /// http://searchhub.org/2009/09/02/scaling-lucene-and-solr#d0e71
        /// Boolean value if norms should be omitted or not. Defaults to false for analyzed fields,
        /// and to true for not_analyzed fields.
        /// </summary>
        public bool OmitNorms { get; set; }

        /// <summary> 
        /// Defaults to Position for analyzed fields, and to DocumentId for not_analyzed fields.
        /// </summary>
        public IndexOptionEnum IndexOptions { get; set; }

        /// <summary>
        /// The analyzer used to analyze the text contents when analyzed during indexing and searching. Defaults to the globally configured analyzer.
        /// </summary>        
        public PropertyAnalyzer Analyzer { get; set; }

        /// <summary>
        /// The analyzer will ignore strings larger than this size. 
        /// Useful for generic not_analyzed fields that should 
        /// ignore long text.
        /// </summary>
        public Int64? IgnoreAbove
        {
            get { return _IgnoreAbove; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value < 0)
                        throw new ArgumentOutOfRangeException("IgnoreAbove", "IgnoreAbove requires a value greater than or equal to zero.");

                    _IgnoreAbove = value;
                }
                else
                    _IgnoreAbove = null;
            }
        }

        /// <summary>
        /// Position increment gap between field instances with the same field name. 
        /// Defaults to 0.
        /// </summary>
        public Int64 PositionOffsetGap { get; set; }

        public StringProperty(string name)
            : base(name, PropertyTypeEnum.String)
        {
            TermVector = _TERM_VECTOR_DEFAULT;
            IndexOptions = _INDEX_OPTION_ANALYZED_DEFAULT;
            OmitNorms = _OMIT_NORMS_ANALYZED_DEFAULT;
            PositionOffsetGap = _POSITION_OFFSET_GAP_DEFAULT;
            DocValues = _DOC_VALUES_DEFAULT;
        }
    }
}

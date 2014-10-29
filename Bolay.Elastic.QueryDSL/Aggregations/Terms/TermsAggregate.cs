using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Terms
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-aggregations-bucket-terms-aggregation.html
    /// </summary>
    [JsonConverter(typeof(TermsSerializer))]
    public class TermsAggregate : BucketAggregationBase
    {
        private int _Size { get; set; }
        private int _ShardSize { get; set; }
        private int _MinimumDocumentCount { get; set; }

        /// <summary>
        /// Gets the field to retrieve terms from.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script value. If fields is populated this is used as a value script. If not it is used as a key script.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Gets or sets the number of terms to return. Setting size to zero will return all terms.
        /// Defaults to 5.
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
        /// Gets or sets the number of terms to retrieve from each shard in the first stage of the request.
        /// Shard size must be greater than or equal to the size.
        /// Defaults to size.
        /// </summary>
        public int ShardSize
        {
            get { return _ShardSize; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("ShardSize", "ShardSize must be greater than zero.");

                _ShardSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to sort on. 
        /// _count for doc_count 
        /// _term for the actual term
        /// This can also be the name of an aggregation, or the name.field an aggregation produces.
        /// </summary>
        public string SortValue { get; set; }

        /// <summary>
        /// Gets or sets the order to sort the "SortValue" in.
        /// Defaults to asc.
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of documents a term must appear if for it to be returned.
        /// Setting this value to zero could return terms from deleted documents.
        /// Defaults to 1.
        /// </summary>
        public int MinimumDocumentCount
        {
            get { return _MinimumDocumentCount; }
            set 
            {
                if (_MinimumDocumentCount < 0)
                    throw new ArgumentOutOfRangeException("MinimumDocumentCount", "MinimumDocumentCount must be greater than or equal to zero.");

                _MinimumDocumentCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the execution_hint value.
        /// </summary>
        public ExecutionTypeEnum ExecutionHint { get; set; }

        /// <summary>
        /// Gets or sets the include regular expression.
        /// </summary>
        public RegexPattern Include { get; set; }

        /// <summary>
        /// Gets or sets the exclude regular expression.
        /// </summary>
        public RegexPattern Exclude { get; set; }

        private TermsAggregate(string name)
            : base(name)
        {
            SortOrder = TermsSerializer._ORDER_DEFAULT;
            Size = TermsSerializer._SIZE_DEFAULT;
            ShardSize = TermsSerializer._SIZE_DEFAULT;
        }

        /// <summary>
        /// Creates a terms aggregation using a field.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the name of the field to retrieve terms from.</param>
        public TermsAggregate(string name, string field)
            : this(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "TermsAggregate requires a field in this constructor.");

            Field = field;
        }

        /// <summary>
        /// Creates a terms aggregation using a field and value script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field.</param>
        /// <param name="script">Sets the value script.</param>
        public TermsAggregate(string name, string field, Script script)
            : this(name, field)
        {
            if (script == null)
                throw new ArgumentNullException("script", "TermsAggregate requires a script in this constructor.");

            Script = script;
        }

        /// <summary>
        /// Creates a terms aggregation using a script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="script">Sets the script.</param>
        public TermsAggregate(string name, Script script)
            : this(name)
        {
            if (script == null)
                throw new ArgumentNullException("script", "TermsAggregate requires a script in this constructor.");

            Script = script;
        }
    }
}

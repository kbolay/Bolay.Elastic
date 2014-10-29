using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.DelimitedPayload
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-delimited-payload-tokenfilter.html
    /// </summary>
    [JsonConverter(typeof(DelimitedPayloadTokenFilterSerializer))]
    public class DelimitedPayloadTokenFilter : TokenFilterBase
    {
        internal const string _DELIMITER_DEFAULT = "|";
        internal static EncodingTypeEnum _ENCODING_DEFAULT = EncodingTypeEnum.Float;

        /// <summary>
        /// Gets or sets the delimiter.
        /// Defaults to '|'.
        /// </summary>
        public string Delimiter { get; set; }
        
        /// <summary>
        /// Gets or sets the encoding.
        /// Defaults to float.
        /// </summary>
        public EncodingTypeEnum Encoding { get; set; }

        /// <summary>
        /// Creates a delimited payload token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        public DelimitedPayloadTokenFilter(string name) : base(name, TokenFilterTypeEnum.DelimitedPayload) { }
    }
}

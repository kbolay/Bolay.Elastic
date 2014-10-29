using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Numbers.TokenCount
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-core-types.html#token_count
    /// </summary>
    [JsonConverter(typeof(TokenCountPropertySerializer))]
    public class TokenCountProperty : NumberProperty
    {
        /// <summary>
        /// Gets the NullValue.
        /// </summary>
        public override object NullValue { get { return null; } set { throw new NotImplementedException(); } }

        /// <summary>
        /// Gets the analyzer to tokenize the fields. 
        /// </summary>
        public string Analyzer { get; private set; }

        public TokenCountProperty(string name, string analyzer)
            : base(name, PropertyTypeEnum.TokenCount)
        {
            if (string.IsNullOrWhiteSpace(analyzer))
                throw new ArgumentNullException("analyzer", "TokenCountProperty requires an analyzer.");

            Analyzer = analyzer;
        }
    }
}

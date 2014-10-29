using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-function-score-query.html#_decay_functions
    /// </summary>
    [JsonConverter(typeof(DecaySerializer))]
    public class DecayFunction : ScoreFunctionBase
    {
        private DecayFunctionsEnum _DECAY_FUNCTION_DEFAULT = DecayFunctionsEnum.Gauss;
        private const Double _DECAY_DEFAULT = 0.5;

        private DecayFunctionsEnum _Function { get; set; }
        private Double? _Decay { get; set; }

        [JsonIgnore]
        public DecayFunctionsEnum Function 
        {
            get
            {
                if (_Function != null)
                    return _Function;
                return _DECAY_FUNCTION_DEFAULT;
            }
            set
            {
                _Function = value;
            }
        }

        [JsonIgnore]
        public string Field { get; set; }

        [JsonProperty("origin")]
        [DefaultValue(null)]
        public string Origin { get; set; }

        [JsonProperty("scale")]
        [DefaultValue(null)]
        public string Scale { get; set; }

        [JsonProperty("offset")]
        [DefaultValue(null)]
        public string Offset { get; set; }

        [JsonProperty("decay")]
        [DefaultValue(_DECAY_DEFAULT)]
        public Double Decay 
        {
            get
            {
                if (_Decay.HasValue)
                    return _Decay.Value;
                return _DECAY_DEFAULT;
            }
            set
            {
                _Decay = value;
            }
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Length : TokenFilterBase
    {
        private const Int32 _MINIMUM_DEFAULT = 0;
        private const Int32 _MAXIMUM_DEFAULT = Int32.MaxValue;

        private int? _Minimum { get; set; }
        private int? _Maximum { get; set; }

        [JsonProperty("min")]
        [DefaultValue(_MINIMUM_DEFAULT)]
        public int Minimum
        {
            get
            {
                if (_Minimum.HasValue)
                    return _Minimum.Value;
                return _MINIMUM_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Minimum", "Must be greater than zero.");

                _Minimum = value;
            }
        }

        [JsonProperty("max")]
        [DefaultValue(_MAXIMUM_DEFAULT)]
        public int Maximum
        {
            get
            {
                if (_Maximum.HasValue)
                    return _Maximum.Value;
                return _MAXIMUM_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Maximum", "Must be greater than zero.");

                _Maximum = value;
            }
        }

        public Length() : base(TokenFilterEnum.Length) { }
    }
}

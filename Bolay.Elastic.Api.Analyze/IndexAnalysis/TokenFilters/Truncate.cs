using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class Truncate : TokenFilterBase
    {
        private const int _LENGTH_DEFAULT = 10;

        private int? _Length { get; set; }

        [JsonProperty("length")]
        [DefaultValue(_LENGTH_DEFAULT)]
        public int Length
        {
            get
            {
                if (_Length.HasValue)
                    return _Length.Value;
                return _LENGTH_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Length", "Must be greater than zero.");

                _Length = value;
            }
        }

        public Truncate() : base(TokenFilterEnum.Truncate) { }
    }
}

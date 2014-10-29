using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class EdgeNGram : TokenFilterBase
    {
        private const Int64 _MINIMUM_SIZE_DEFAULT = 1;
        private const Int64 _MAXIMUM_SIZE_DEFAULT = 2;
        private const string _SIDE_DEFAULT = "front";

        private Int64? _MinimumSize { get; set; }
        private Int64? _MaximumSize { get; set; }
        private SideEnum _Side { get; set; }

        [JsonProperty("min_gram")]
        [DefaultValue(_MINIMUM_SIZE_DEFAULT)]
        public Int64 MinimumSize
        {
            get
            {
                if (_MinimumSize.HasValue)
                    return _MinimumSize.Value;
                return _MINIMUM_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MinimumSize", "Must be greater than zero.");

                _MinimumSize = value;
            }
        }

        [JsonProperty("max_gram")]
        [DefaultValue(_MAXIMUM_SIZE_DEFAULT)]
        public Int64 MaximumSize
        {
            get
            {
                if (_MaximumSize.HasValue)
                    return _MaximumSize.Value;
                return _MAXIMUM_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaximumSize", "Must be greater than zero.");

                _MaximumSize = value;
            }
        }

        [JsonProperty("side")]
        [DefaultValue(_SIDE_DEFAULT)]
        public string Side
        {
            get
            {
                if (_Side != null)
                    return _Side.ToString();
                return _SIDE_DEFAULT;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SideEnum side = SideEnum.Front;
                    side = SideEnum.Find(value);
                    if (side != null)
                        _Side = side;
                    else
                        throw new ArgumentOutOfRangeException("Side", "Not a valid side value.");
                }
                else
                    _Side = SideEnum.Front;
            }
        }

        public EdgeNGram() : base(TokenFilterEnum.EdgeNGram) { }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public class PathHierarchy : TokenizerBase
    {
        private const string _DELIMITER_DEFAULT = "/";
        private const Int64 _BUFFER_SIZE_DEFAULT = 1024;
        private const Int64 _SKIP_DEFAULT = 0;

        private string _Delimeter { get; set; }
        private string _Replacement { get; set; }
        private Int64? _BufferSize { get; set; }
        private Int64? _Skip { get; set; }

        [JsonProperty("delimeter")]
        [DefaultValue(_DELIMITER_DEFAULT)]
        public string Delimeter
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_Delimeter))
                    return _Delimeter;
                return _DELIMITER_DEFAULT;
            }
            set
            {
                _Delimeter = value;
            }
        }

        [JsonProperty("replacement")]
        [DefaultValue(default(string))]
        public string Replacement
        {
            get 
            {
                if (_Replacement != default(string) && !_Replacement.Equals(_Delimeter))
                    return _Replacement;
                return default(string);
            }
            set
            {
                _Replacement = value;
            }
        }

        [JsonProperty("buffer_size")]
        [DefaultValue(_BUFFER_SIZE_DEFAULT)]
        public Int64 BufferSize 
        {
            get
            {
                if (_BufferSize.HasValue)
                    return _BufferSize.Value;
                return _BUFFER_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("BufferSize", "Must be greater than zero.");
                _BufferSize = value;
            }
        }

        [JsonProperty("reverse")]
        [DefaultValue(default(bool))]
        public bool Reverse { get; set; }

        [JsonProperty("skip")]
        [DefaultValue(default(Int64))]
        public Int64 Skip
        {
            get
            {
                if(_Skip.HasValue)
                    return _Skip.Value;
                return default(Int64);
            }
            set
            {
                if(value <= 0)
                    throw new ArgumentOutOfRangeException("Skip", "Must be greater than zero.");
                _Skip = value;
            }
        }

        public PathHierarchy() : base(TokenizerEnum.PathHierarchy) { }
    }
}

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Standard : AnalyzerBase
    {
        private const Int64 _MAX_TOKEN_LENGTH_DEFAULT = 255;

        private Int64? _MaxTokenLength { get; set; }

        [JsonProperty("stopwords")]
        [DefaultValue(default(List<string>))]
        public List<string> StopWords { get; set; }

        [JsonProperty("max_token_length")]
        [DefaultValue(_MAX_TOKEN_LENGTH_DEFAULT)]
        public Int64 MaxTokenLength 
        {
            get 
            {
                if (_MaxTokenLength.HasValue)
                    return _MaxTokenLength.Value;

                return _MAX_TOKEN_LENGTH_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MaxTokenLength");

                _MaxTokenLength = value;
            }
        }

        public Standard() : base(AnalyzerEnum.Standard) { }
    }
}

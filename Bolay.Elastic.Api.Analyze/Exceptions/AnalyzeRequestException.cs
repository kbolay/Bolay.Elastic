using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Exceptions
{
    public class AnalyzeRequestException : Exception
    {
        public Uri AnalyzeUri { get; set; }

        public AnalyzeRequestException(Uri analyzeUri) : this(analyzeUri, null, null) { }
        public AnalyzeRequestException(Uri analyzeUri, string message) : this(analyzeUri, message, null) { }
        public AnalyzeRequestException(Uri analyzeUri, string message, Exception innerException)
            : base(message, innerException)
        {
            this.AnalyzeUri = analyzeUri;
        }
    }
}

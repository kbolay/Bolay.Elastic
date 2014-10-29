using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    public class FieldAnalyzer
    {
        public string OverrideName { get; set; }
        public string AnalyzerName { get; set; }
        public string IndexAnalyzer { get; set; }
        public string SearchAnalyzer { get; set; }
    }
}

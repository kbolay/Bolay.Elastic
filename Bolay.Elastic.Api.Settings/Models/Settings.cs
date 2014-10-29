using Bolay.Elastic.Analysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Settings.Models
{
    [JsonConverter(typeof(SettingsSerializer))]
    public class Settings
    {
        public string IndexName { get; set; }
        public string UUID { get; set; }
        public int Replicas { get; set; }
        public int Shards { get; set; }
        public AnalysisSettings Analysis { get; set; }
        public Version Version { get; set; }
    }
}

using Bolay.Elastic.Analysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Settings.Models
{
    internal class SettingsSerializer : JsonConverter
    {
        private const string _ANALYSIS = "analysis";
        private const string _UUID = "uuid";
        private const string _NUMBER_OF_REPLICAS = "number_of_replicas";
        private const string _NUMBER_OF_SHARDS = "number_of_shards";
        private const string _VERSION = "version";
        private const string _CREATED = "created";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> indexDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            Settings indexSettings = new Settings();
            indexSettings.IndexName = indexDict.First().Key;

            while (!indexDict.ContainsKey(_ANALYSIS))
            {
                indexDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(indexDict.First().Value.ToString());
            }

            indexSettings.Analysis = JsonConvert.DeserializeObject<AnalysisSettings>(indexDict.GetString(_ANALYSIS));
            indexSettings.Replicas = indexDict.GetInt32(_NUMBER_OF_REPLICAS);
            indexSettings.Shards = indexDict.GetInt32(_NUMBER_OF_SHARDS);
            indexSettings.UUID = indexDict.GetString(_UUID);
            indexSettings.Version = new Version();

            Dictionary<string, object> versionDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(indexDict.GetString(_VERSION));
            indexSettings.Version.Created = versionDict.GetInt64(_CREATED);

            return indexSettings;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

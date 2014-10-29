using Bolay.Elastic.Analysis;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._All
{
    public class AllSerializer : JsonConverter
    {
        private const string _IS_ENABLED = "enabled";
        private const string _STORE = "store";
        private const string _INCLUDES = "includes";
        private const string _EXCLUDES = "excludes";
        private const string _TERM_VECTOR = "term_vector";
        
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            All all = new All();
            all.Analyzer = PropertyAnalyzer.Deserialize(fieldDict);
            if (fieldDict.ContainsKey(_EXCLUDES))
                all.Excludes = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_EXCLUDES));
            if (fieldDict.ContainsKey(_INCLUDES))
                all.Includes = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_INCLUDES));
            all.IsEnabled = fieldDict.GetBool(_IS_ENABLED, All._IS_ENABLED_DEFAULT);
            all.Store = fieldDict.GetBool(_STORE, All._STORE_DEFAULT);
            all.TermVector = TermVectorEnum.Find(fieldDict.GetString(_TERM_VECTOR));

            return all;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is All))
                throw new SerializeTypeException<All>();

            All all = value as All;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_IS_ENABLED, all.IsEnabled, All._IS_ENABLED_DEFAULT);
            fieldDict.AddObject(_STORE, all.Store, All._STORE_DEFAULT);
            fieldDict.AddObject(_TERM_VECTOR, all.TermVector.ToString(), All._TERM_VECTOR_DEFAULT.ToString());
            fieldDict.AddObject(_INCLUDES, all.Includes);
            fieldDict.AddObject(_EXCLUDES, all.Excludes);
            PropertyAnalyzer.Serialize(all.Analyzer, fieldDict);

            serializer.Serialize(writer, fieldDict);
        }
    }
}

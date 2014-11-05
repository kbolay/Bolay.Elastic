using Bolay.Elastic.Analysis;
using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.String
{
    internal class StringPropertySerializer : JsonConverter
    {
        private const string _DOC_VALUES = "doc_values";
        private const string _TERM_VECTOR = "term_vector";
        private const string _INDEX_OPTIONS = "index_options";
        private const string _IGNORE_ABOVE = "ignore_above";
        private const string _POSITION_GAP_OFFSET = "position_gap_offset";
        private const string _OMIT_NORMS = "omit_norms";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> propDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            string name = propDict.First().Key;
            StringProperty property = new StringProperty(name);

            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(propDict.First().Value.ToString());
            FieldProperty.Deserialize(property, fieldDict);

            property.Analyzer = PropertyAnalyzer.Deserialize(fieldDict);
            property.DocValues = fieldDict.GetBool(_DOC_VALUES, StringProperty._DOC_VALUES_DEFAULT);
            if (property.IsAnalyzed)
            {
                property.IndexOptions = IndexOptionEnum.Find(fieldDict.GetString(_INDEX_OPTIONS, StringProperty._INDEX_OPTION_ANALYZED_DEFAULT.ToString()));
                property.Norms = Norms.Deserialize(fieldDict);
                property.OmitNorms = fieldDict.GetBool(_OMIT_NORMS, StringProperty._OMIT_NORMS_ANALYZED_DEFAULT);
            }
            else
            {
                property.IndexOptions = IndexOptionEnum.Find(fieldDict.GetString(_INDEX_OPTIONS, StringProperty._INDEX_OPTION_NOT_ANALYZED_DEFAULT.ToString()));
                property.Norms = Norms.Deserialize(fieldDict, false);
                property.OmitNorms = fieldDict.GetBool(_OMIT_NORMS, StringProperty._OMIT_NORMS_NOT_ANALYZED_DEFAULT);
            }

            property.IgnoreAbove = fieldDict.GetInt64OrNull(_IGNORE_ABOVE);
            property.PositionOffsetGap = fieldDict.GetInt64(_POSITION_GAP_OFFSET, StringProperty._POSITION_OFFSET_GAP_DEFAULT);
            property.TermVector = TermVectorEnum.Find(fieldDict.GetString(_TERM_VECTOR, StringProperty._TERM_VECTOR_DEFAULT.ToString()));

            return property;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is StringProperty))
                throw new SerializeTypeException<StringProperty>();

            StringProperty property = value as StringProperty;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            FieldProperty.Serialize(property, fieldDict);
            PropertyAnalyzer.Serialize(property.Analyzer, fieldDict);
            fieldDict.AddObject(_DOC_VALUES, property.DocValues, StringProperty._DOC_VALUES_DEFAULT);
            fieldDict.AddObject(_IGNORE_ABOVE, property.IgnoreAbove);
            if (property.IsAnalyzed)
            {
                fieldDict.AddObject(_INDEX_OPTIONS, property.IndexOptions.ToString(), StringProperty._INDEX_OPTION_ANALYZED_DEFAULT.ToString());
                fieldDict.AddObject(_OMIT_NORMS, property.OmitNorms, StringProperty._OMIT_NORMS_ANALYZED_DEFAULT);
            }
            else
            {
                fieldDict.AddObject(_INDEX_OPTIONS, property.IndexOptions.ToString(), StringProperty._INDEX_OPTION_NOT_ANALYZED_DEFAULT.ToString());
                fieldDict.AddObject(_OMIT_NORMS, property.OmitNorms, StringProperty._OMIT_NORMS_NOT_ANALYZED_DEFAULT);
            }
                
            Norms.Serialize(property.Norms, fieldDict, property.IsAnalyzed);
            fieldDict.AddObject(_POSITION_GAP_OFFSET, property.PositionOffsetGap, StringProperty._POSITION_OFFSET_GAP_DEFAULT);
            fieldDict.AddObject(_TERM_VECTOR, property.TermVector.ToString(), StringProperty._TERM_VECTOR_DEFAULT.ToString());

            Dictionary<string, object> propDict = new Dictionary<string, object>();
            propDict.Add(property.Name, fieldDict);

            serializer.Serialize(writer, propDict);
        }
    }
}

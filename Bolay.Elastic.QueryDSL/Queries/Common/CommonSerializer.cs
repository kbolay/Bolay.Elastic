using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Common
{
    public class CommonSerializer : JsonConverter
    {
        private const string _COMMON = "common";
        private const string _BODY = "body";
        private const string _QUERY = "query";
        private const string _CUTOFF_FREQUENCY = "cutoff_frequency";
        private const string _HIGH_FREQUENCY_OPERATOR = "high_freq_operator";
        private const string _LOW_FREQUENCY_OPERATOR = "low_freq_operator";
        private const string _MINIMUM_SHOULD_MATCH = "minimum_should_match";
        private const string _HIGH_FREQ_MINIMUM = "high_freq";
        private const string _LOW_FREQ_MINIMUM = "high_freq";

        internal static OperatorEnum _HIGH_FREQUENCY_OPERATOR_DEFAULT = OperatorEnum.Or;
        internal static OperatorEnum _LOW_FREQUENCY_OPERATOR_DEFAULT = OperatorEnum.Or;
        internal const int _MINIMUM_SHOULD_MATCH_DEFAULT = 1;
        internal const Double _CUTOFF_FREQUENCY_DEFAULT = 0.001;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            while(!fieldDict.ContainsKey(_QUERY))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            CommonQuery query = new CommonQuery();

            query.Query = fieldDict.GetString(_QUERY);
            query.CutOffFrequency = fieldDict.GetDouble(_CUTOFF_FREQUENCY, _CUTOFF_FREQUENCY_DEFAULT);
            query.HighFrequencyOperator = OperatorEnum.Find(fieldDict.GetString(_HIGH_FREQUENCY_OPERATOR, _HIGH_FREQUENCY_OPERATOR_DEFAULT.ToString()));
            query.LowFrequencyOperator = OperatorEnum.Find(fieldDict.GetString(_LOW_FREQUENCY_OPERATOR, _LOW_FREQUENCY_OPERATOR_DEFAULT.ToString()));
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            if (fieldDict.ContainsKey(_MINIMUM_SHOULD_MATCH))
            {
                Dictionary<string, object> minimumDict = null;
                try
                {
                    minimumDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict[_MINIMUM_SHOULD_MATCH].ToString());
                }
                catch { }

                if (minimumDict != null)
                {
                    MinimumShouldMatchBase highMin = null;
                    MinimumShouldMatchBase lowMin = null;
                    if (minimumDict.ContainsKey(_HIGH_FREQ_MINIMUM))
                        highMin = MinimumShouldMatchBase.BuildMinimumShouldMatch(minimumDict[_HIGH_FREQ_MINIMUM].ToString());
                    if (minimumDict.ContainsKey(_LOW_FREQ_MINIMUM))
                        lowMin = MinimumShouldMatchBase.BuildMinimumShouldMatch(minimumDict[_LOW_FREQ_MINIMUM].ToString());

                    query.MinimumShouldMatch = new MinimumShouldMatch(highMin, lowMin);
                }
                else
                {
                    MinimumShouldMatchBase allMin = MinimumShouldMatchBase.BuildMinimumShouldMatch(minimumDict[_MINIMUM_SHOULD_MATCH].ToString());
                    query.MinimumShouldMatch = new MinimumShouldMatch(allMin);
                }
            }

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(!(value is CommonQuery))
                throw new SerializeTypeException<CommonQuery>();

            CommonQuery query = value as CommonQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.Add(_QUERY, query.Query);
            fieldDict.Add(_CUTOFF_FREQUENCY, query.CutOffFrequency);

            fieldDict.AddObject(_HIGH_FREQUENCY_OPERATOR, query.HighFrequencyOperator.ToString(), _HIGH_FREQUENCY_OPERATOR_DEFAULT.ToString());
            fieldDict.AddObject(_LOW_FREQUENCY_OPERATOR, query.LowFrequencyOperator.ToString(), _LOW_FREQUENCY_OPERATOR_DEFAULT.ToString());

            if (query.MinimumShouldMatch != null)
            {
                if (query.MinimumShouldMatch.HighFrequency != null || query.MinimumShouldMatch.LowFrequency != null)
                {
                    Dictionary<string, object> minimumDict = new Dictionary<string, object>();
                    if (query.MinimumShouldMatch.HighFrequency != null &&
                        !query.MinimumShouldMatch.HighFrequency.GetValue().Equals(_MINIMUM_SHOULD_MATCH_DEFAULT))
                    {
                        minimumDict.Add(_HIGH_FREQ_MINIMUM, query.MinimumShouldMatch.HighFrequency);
                    }
                    if (query.MinimumShouldMatch.LowFrequency != null &&
                        !query.MinimumShouldMatch.LowFrequency.GetValue().Equals(_MINIMUM_SHOULD_MATCH_DEFAULT))
                    {
                        minimumDict.Add(_LOW_FREQ_MINIMUM, query.MinimumShouldMatch.LowFrequency);
                    }

                    fieldDict.Add(_MINIMUM_SHOULD_MATCH, minimumDict);
                }
                else if(query.MinimumShouldMatch.All != null && 
                    !query.MinimumShouldMatch.All.GetValue().Equals(_MINIMUM_SHOULD_MATCH_DEFAULT))
                {
                    fieldDict.Add(_MINIMUM_SHOULD_MATCH, query.MinimumShouldMatch.All.GetValue());
                }
            }

            fieldDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);

            Dictionary<string, object> bodyDict = new Dictionary<string, object>();
            bodyDict.Add(_BODY, fieldDict);

            Dictionary<string, object> commonDict = new Dictionary<string, object>();
            commonDict.Add(_COMMON, bodyDict);

            serializer.Serialize(writer, commonDict);
        }
    }
}

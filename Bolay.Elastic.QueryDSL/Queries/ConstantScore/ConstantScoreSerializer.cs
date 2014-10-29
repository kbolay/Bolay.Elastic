using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.ConstantScore
{
    public class ConstantScoreSerializer : JsonConverter
    {
        private const string _CONSTANT_SCORE = "constant_score";
        private const string _QUERY = "query";
        private const string _FILTER = "filter";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);

            if (fieldDict.ContainsKey(_CONSTANT_SCORE))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            ConstantScoreQueryBase constantScore = null;

            if (fieldDict.ContainsKey(_QUERY))
            { 
                Dictionary<string, object> queryDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict[_QUERY].ToString());
                QueryTypeEnum queryType = QueryTypeEnum.ConstantScoreQuery;
                queryType = QueryTypeEnum.Find(queryDict.First().Key);
                IQuery query = JsonConvert.DeserializeObject(queryDict.First().Value.ToString(), queryType.ImplementationType) as IQuery;
                constantScore = new ConstantScoreQuery(query);
            }
            else if (fieldDict.ContainsKey(_FILTER))
            {
                Dictionary<string, object> filterDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict[_FILTER].ToString());
                FilterTypeEnum filterType = FilterTypeEnum.Term;
                filterType = FilterTypeEnum.Find(filterDict.First().Key);
                IFilter filter = JsonConvert.DeserializeObject(filterDict.First().Value.ToString(), filterType.ImplementationType) as IFilter;
                constantScore = new ConstantScoreFilter(filter);
            }
            else 
            {
                throw new Exception("ConstantScore expects a filter or query.");
            }

            constantScore.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            constantScore.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return constantScore;
        }   

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is ConstantScoreQueryBase))
                throw new SerializeTypeException<ConstantScoreQueryBase>();

            ConstantScoreQueryBase query = value as ConstantScoreQueryBase;

            Dictionary<string, object> constantDict = new Dictionary<string, object>();
            Dictionary<string, object> internalDict = new Dictionary<string, object>();
            if (value is ConstantScoreQuery)
            {
                internalDict.Add(_QUERY, query.SearchPiece);
            }
            else if (value is ConstantScoreFilter)
            {
                internalDict.Add(_FILTER, query.SearchPiece);
            }
            else
            {
                throw new Exception("Serialization value is not a ConstantScoreQuery or ConstantScoreFilter.");
            }

            internalDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
            internalDict.AddObject(QuerySerializer._QUERY_NAME, query.QueryName);
            constantDict.Add(_CONSTANT_SCORE, internalDict);

            serializer.Serialize(writer, constantDict);
        }
    }
}

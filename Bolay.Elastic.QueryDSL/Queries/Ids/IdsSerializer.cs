using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Ids
{
    public class IdsSerializer : JsonConverter
    {
        private const string _TYPE = "type";
        private const string _IDS = "values";

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(QueryTypeEnum.Ids.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            List<string> ids = JsonConvert.DeserializeObject<List<string>>(fieldDict.GetString(_IDS));
            IdsQuery query = new IdsQuery(ids);

            if (fieldDict.ContainsKey(_TYPE))
            {
                try
                {
                    List<string> types = JsonConvert.DeserializeObject<List<string>>(fieldDict[_TYPE].ToString());
                    query.DocumentTypes = types;
                }
                catch
                {
                    if(!string.IsNullOrWhiteSpace(fieldDict[_TYPE].ToString()))
                    {
                        query.DocumentTypes = new List<string>()
                        {
                            fieldDict[_TYPE].ToString()
                        };
                    }
                    
                }
            }

            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);

            return query;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IdsQuery))
                throw new SerializeTypeException<IdsQuery>();

            IdsQuery query = value as IdsQuery;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if(query.DocumentTypes != null && query.DocumentTypes.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                if(query.DocumentTypes.Count(x => !string.IsNullOrWhiteSpace(x)) == 1)
                    fieldDict.Add(_TYPE, query.DocumentTypes.First(x => !string.IsNullOrWhiteSpace(x)));
                else
                    fieldDict.Add(_TYPE, query.DocumentTypes.Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            fieldDict.Add(_IDS, query.DocumentIds);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add(QueryTypeEnum.Ids.ToString(), fieldDict);

            serializer.Serialize(writer, queryDict);
        }
    }
}

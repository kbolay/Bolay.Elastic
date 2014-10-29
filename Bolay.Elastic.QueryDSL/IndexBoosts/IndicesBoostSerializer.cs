using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.IndexBoosts
{
    public class IndicesBoostSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fieldDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fieldDict.ContainsKey(SearchPieceTypeEnum.IndicesBoost.ToString()))
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.First().Value.ToString());

            List<IndexBoost> indexBoostList = new List<IndexBoost>();
            foreach (KeyValuePair<string, object> indexBoostKvp in fieldDict)
            {
                indexBoostList.Add(new IndexBoost(indexBoostKvp.Key, Convert.ToDouble(indexBoostKvp.Value)));
            }

            return new IndicesBoost(indexBoostList);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IndicesBoost))
                throw new SerializeTypeException<IndicesBoost>();

            IndicesBoost indicesBoost = value as IndicesBoost;

            Dictionary<string, object> boostDict = new Dictionary<string, object>();
            foreach (IndexBoost boost in indicesBoost.BoostedIndices)
            {
                boostDict.Add(boost.Index, boost.Boost);
            }

            Dictionary<string, object> indicesBoostDict = new Dictionary<string, object>();
            indicesBoostDict.Add(SearchPieceTypeEnum.IndicesBoost.ToString(), boostDict);

            serializer.Serialize(writer, indicesBoostDict);
        }
    }
}

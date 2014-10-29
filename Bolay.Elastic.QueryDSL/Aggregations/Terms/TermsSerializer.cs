using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Models;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations.Terms
{
    public class TermsSerializer : JsonConverter
    {
        private const string _FLAG_DELIMITER = "|";
        private static List<string> _FLAG_DELIMITERS = new List<string>() { " | ", "| ", " |", "|" };

        private const string _FIELD = "field";
        private const string _INCLUDE = "include";
        private const string _EXCLUDE = "exclude";
        private const string _SIZE = "size";
        private const string _SHARD_SIZE = "shard_size";
        private const string _FLAGS = "flags";
        private const string _PATTERN = "pattern";
        private const string _EXECUTION_HINT = "execution_hint";
        private const string _MINIMUM_DOCUMENT_COUNT = "min_doc_count";
        private const string _ORDER = "order";

        internal static SortOrderEnum _ORDER_DEFAULT = SortOrderEnum.Ascending;
        internal const int _SIZE_DEFAULT = 5;
        internal const int _MINIMUM_DOCUMENT_COUNT_DEFAULT = 1;
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> wholeDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wholeDict.First().Value.ToString());
            Dictionary<string, object> fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(AggregationTypeEnum.Terms.ToString()));

            string aggName = wholeDict.First().Key;
            string field = fieldDict.GetStringOrDefault(_FIELD);
            Script script = ScriptSerializer.Deserialize(fieldDict);

            TermsAggregate agg = null;
            if (!string.IsNullOrWhiteSpace(field) && script != null)
                agg = new TermsAggregate(aggName, field, script);
            else if (!string.IsNullOrWhiteSpace(field))
                agg = new TermsAggregate(aggName, field);
            else if (script != null)
                agg = new TermsAggregate(aggName, script);
            else
                throw new RequiredPropertyMissingException(_FIELD + "/" + ScriptSerializer._SCRIPT);

            if(fieldDict.ContainsKey(_EXECUTION_HINT))
            {
                ExecutionTypeEnum map = ExecutionTypeEnum.Map;
                agg.ExecutionHint = ExecutionTypeEnum.Find(fieldDict.GetString(_EXECUTION_HINT));
            }

            agg.MinimumDocumentCount = fieldDict.GetInt32(_MINIMUM_DOCUMENT_COUNT, _MINIMUM_DOCUMENT_COUNT_DEFAULT);
            agg.Size = fieldDict.GetInt32(_SIZE, _SIZE_DEFAULT);
            agg.ShardSize = fieldDict.GetInt32(_SHARD_SIZE, agg.Size);

            if (fieldDict.ContainsKey(_ORDER))
            {
                Dictionary<string, object> orderDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_ORDER));
                if (orderDict.Count != 1)
                    throw new Exception("The order parameter must be a dictionary of one key value pair.");

                agg.SortValue = orderDict.First().Key;
                agg.SortOrder = SortOrderEnum.Find(orderDict.First().Value.ToString());
            }

            if (fieldDict.ContainsKey(_EXCLUDE))
            {
                agg.Exclude = DeserializeRegexPattern(fieldDict.GetString(_EXCLUDE));
            }
            if (fieldDict.ContainsKey(_INCLUDE))
            {
                agg.Include = DeserializeRegexPattern(fieldDict.GetString(_INCLUDE));
            }

            agg.SubAggregations = BucketAggregationBase.DeserializeSubAggregations(aggDict);
            return agg;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TermsAggregate))
                throw new SerializeTypeException<TermsAggregate>();

            TermsAggregate agg = value as TermsAggregate;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_FIELD, agg.Field);
            ScriptSerializer.Serialize(agg.Script, fieldDict);
            fieldDict.AddObject(_SIZE, agg.Size, _SIZE_DEFAULT);
            fieldDict.AddObject(_SHARD_SIZE, agg.ShardSize, agg.Size);
            fieldDict.AddObject(_MINIMUM_DOCUMENT_COUNT, agg.MinimumDocumentCount, _MINIMUM_DOCUMENT_COUNT_DEFAULT);

            if (!string.IsNullOrWhiteSpace(agg.SortValue))
            {
                Dictionary<string, object> orderDict = new Dictionary<string, object>();
                orderDict.Add(agg.SortValue, agg.SortOrder.ToString());
                fieldDict.Add(_ORDER, orderDict);
            }

            if (agg.Include != null)
            { 
                if(agg.Include.Flags != null && agg.Include.Flags.Any())
                    fieldDict.Add(_INCLUDE, SerializeRegexPattern(agg.Include));
                else
                    fieldDict.Add(_INCLUDE, agg.Include.Pattern);
            }

            if (agg.Exclude != null)
            { 
                if(agg.Exclude.Flags != null && agg.Exclude.Flags.Any())
                    fieldDict.Add(_EXCLUDE, SerializeRegexPattern(agg.Exclude));
                else
                    fieldDict.Add(_EXCLUDE, agg.Exclude.Pattern);
            }

            fieldDict.AddObject(_EXECUTION_HINT, agg.ExecutionHint.ToString());

            Dictionary<string, object> aggDict = new Dictionary<string, object>();
            aggDict.Add(AggregationTypeEnum.Terms.ToString(), fieldDict);

            Dictionary<string, object> subAggsDict = agg.SerializeSubAggregations();
            if (subAggsDict != null)
            {
                aggDict.Add(BucketAggregationBase._SUB_AGGREGATIONS, subAggsDict);
            }

            Dictionary<string, object> aggNameDict = new Dictionary<string, object>();
            aggNameDict.Add(agg.Name, aggDict);

            serializer.Serialize(writer, aggNameDict);
        }

        private RegexPattern DeserializeRegexPattern(string json)
        { 
            Dictionary<string, object> regexDict = null;
            try
            {
                regexDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            catch 
            { 
                return new RegexPattern(json);
            }

            List<RegexFlagEnum> regexFlags = new List<RegexFlagEnum>();
            if(regexDict.ContainsKey(_FLAGS))
            {
                string[] flagStrings = regexDict.GetString(_FLAGS).Split(_FLAG_DELIMITERS.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                RegexFlagEnum flagType = RegexFlagEnum.CannonEq;
                foreach (string flagStr in flagStrings)
                {
                    flagType = RegexFlagEnum.Find(flagStr);
                    if (flagType == null)
                        throw new Exception(flagStr + " is not a valid RegexFlag.");
                    regexFlags.Add(flagType);
                }
            }
            
            if (regexFlags.Any())
                return new RegexPattern(regexDict.GetString(_PATTERN), regexFlags);

            return new RegexPattern(regexDict.GetString(_PATTERN));
        }
        private Dictionary<string, object> SerializeRegexPattern(RegexPattern regexPattern)
        {
            Dictionary<string, object> regexDict = new Dictionary<string, object>();
            regexDict.Add(_PATTERN, regexPattern.Pattern);

            if (regexPattern.Flags == null || !regexPattern.Flags.Any())
                return regexDict;

            StringBuilder flagsBuilder = new StringBuilder();
            foreach (RegexFlagEnum flag in regexPattern.Flags)
            {
                if (flagsBuilder.Length > 0)
                    flagsBuilder.Append(_FLAG_DELIMITER);

                flagsBuilder.Append(flag.ToString());
            }

            regexDict.Add(_FLAGS, flagsBuilder.ToString());

            return regexDict;
        }
    }
}

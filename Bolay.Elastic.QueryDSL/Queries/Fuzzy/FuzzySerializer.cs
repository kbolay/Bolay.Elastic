using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Fuzzy
{
    public class FuzzySerializer : JsonConverter
    {
        private const string _PREFIX_LENGTH = "prefix_length";
        private const string _MAXIMUM_EXPANSIONS = "max_expansions";
        private const string _FUZZINESS = "fuzziness";
        private const string _VALUE = "value";

        internal const int _PREFIX_LENGTH_DEFAULT = default(int);
        internal const int _MAXIMUM_EXPANSIONS_DEFAULT = default(int);

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> fuzzyDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (fuzzyDict.ContainsKey(QueryTypeEnum.Fuzzy.ToString()))
                fuzzyDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fuzzyDict.First().Value.ToString());


            return BuildFuzzyQuery(fuzzyDict.First());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FuzzyQueryBase))
                throw new SerializeTypeException<FuzzyQueryBase>();

            FuzzyQueryBase query = value as FuzzyQueryBase;
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();

            if (query.Boost == QuerySerializer._BOOST_DEFAULT && query.Fuzziness == null && query.MaximumExpansions == default(int) && query.PrefixLength == default(int))
            {
                fieldDict.Add(query.Field, query.Value);
            }
            else
            {
                Dictionary<string, object> detailDict = new Dictionary<string, object>();
                detailDict.Add(_VALUE, query.Value);
                detailDict.AddObject(QuerySerializer._BOOST, query.Boost, QuerySerializer._BOOST_DEFAULT);
                detailDict.AddObject(_FUZZINESS, query.Fuzziness);
                detailDict.AddObject(_PREFIX_LENGTH, query.PrefixLength, _PREFIX_LENGTH_DEFAULT);
                detailDict.AddObject(_MAXIMUM_EXPANSIONS, query.MaximumExpansions, _MAXIMUM_EXPANSIONS_DEFAULT);
                
                fieldDict.Add(query.Field, detailDict);
            }

            Dictionary<string, object> fuzzyDict = new Dictionary<string, object>();
            fuzzyDict.Add(QueryTypeEnum.Fuzzy.ToString(), fieldDict);

            serializer.Serialize(writer, fuzzyDict);
        }

        private FuzzyQueryBase BuildFuzzyQuery(KeyValuePair<string, object> fuzzyKvp)
        {
            FuzzyQueryBase query = null;
            string field = fuzzyKvp.Key;
            Dictionary<string, object> fieldDict = null;
            try
            {
                fieldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fuzzyKvp.Value.ToString());
            }
            catch
            {                
                object value = fuzzyKvp.Value;
                if (IsDateTime(value))
                    return new FuzzyDateQuery(field, DateTime.Parse(value.ToString()));
                else if (IsNumeric(value))
                {
                    if (value is Int64 || value is Int32)
                        return new FuzzyNumberQuery(field, (Int64)value)
                        {
                            QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME)
                        };
                    else
                        return new FuzzyNumberQuery(field, (Double)value)
                        {
                            QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME)
                        };
                }

                return new FuzzyStringQuery(field, value.ToString())
                        {
                            QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME)
                        };
            }

            return BuildFuzzyQuery(field, fieldDict);
        }
        private FuzzyQueryBase BuildFuzzyQuery(string fieldName, Dictionary<string, object> fieldDict)
        {
            object value = fieldDict[_VALUE];
            object fuzziness = null;
            if (fieldDict.ContainsKey(_FUZZINESS))
                fuzziness = fieldDict[_FUZZINESS];

            FuzzyQueryBase query = null;
            if (IsDateTime(value))
            {
                if(fuzziness != null)
                    query = new FuzzyDateQuery(fieldName, DateTime.Parse(value.ToString()), new TimeValue(fuzziness.ToString()).TimeSpan);
                else
                    query = new FuzzyDateQuery(fieldName, DateTime.Parse(value.ToString()));
            }
            else if (IsNumeric(value))
            {
                bool isInteger = false;
                if (value is Int64 || value is Int32)
                    isInteger = true;

                if (fuzziness != null && isInteger)
                {
                    if (isInteger)
                        query = new FuzzyNumberQuery(fieldName, (Int64)value, (Int64)fuzziness);
                    else
                        query = new FuzzyNumberQuery(fieldName, (Double)value, (Double)fuzziness);
                }
                else
                {
                    if (isInteger)
                        query = new FuzzyNumberQuery(fieldName, (Int64)value);
                    else
                        query = new FuzzyNumberQuery(fieldName, (Double)value);
                }
            }
            else
            {
                if (fuzziness != null)
                    query = new FuzzyStringQuery(fieldName, value.ToString(), Int32.Parse(fuzziness.ToString()));
                else
                    query = new FuzzyStringQuery(fieldName, value.ToString());
            }

            query.Boost = fieldDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            query.MaximumExpansions = fieldDict.GetInt32(_MAXIMUM_EXPANSIONS, _MAXIMUM_EXPANSIONS_DEFAULT);
            query.PrefixLength = fieldDict.GetInt32(_PREFIX_LENGTH, _PREFIX_LENGTH_DEFAULT);
            query.QueryName = fieldDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return query;
        }
        private bool IsNumeric(object value)
        { 
            Double doubleValue = 0;
            if (Double.TryParse(value.ToString(), out doubleValue))
                return true;
            
            return false;
        }
        private bool IsDateTime(object value)
        {
            DateTime dateTimeValue = new DateTime();
            if (DateTime.TryParse(value.ToString(), out dateTimeValue))
                return true;

            return false;
        }
    }
}

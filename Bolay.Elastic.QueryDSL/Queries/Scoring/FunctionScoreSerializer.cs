using Bolay.Elastic.Exceptions;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Queries.Scoring.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring
{
    public class FunctionScoreSerializer : JsonConverter
    {
        private const string _QUERY = "query";
        private const string _FILTER = "filter";
        private const string _FUNCTIONS = "functions";
        private const string _FUNCTION_SCORE = "function_score";
        private const string _MAX_BOOST = "max_boost";
        private const string _SCORE_MODE = "score_mode";
        private const string _BOOST_MODE = "boost_mode";

        internal static ScoreModeEnum _SCORE_MODE_DEFAULT = ScoreModeEnum.Multiply;
        internal static BoostModeEnum _BOOST_MODE_DEFAULT = BoostModeEnum.Multiply;
        internal const float _MAX_BOOST_DEFAULT = float.MaxValue;

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Dictionary<string, object> scoreDict = serializer.Deserialize<Dictionary<string, object>>(reader);
            if (scoreDict.ContainsKey(_FUNCTION_SCORE))
                scoreDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(scoreDict.GetString(_FUNCTION_SCORE));

            List<ScoreFunctionBase> functionList = new List<ScoreFunctionBase>();
            if(scoreDict.ContainsKey(_FUNCTIONS))
            {
                List<Dictionary<string, object>> functionsDictList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(scoreDict.GetString(_FUNCTIONS));
                
                foreach(Dictionary<string, object> functionDict in functionsDictList)
                {
                    IFilter filter = null;
                    if(functionDict.ContainsKey(_FILTER))
                        filter = JsonConvert.DeserializeObject<IFilter>(functionDict.GetString(_FILTER));

                    ScoreFunctionEnum functionEnum = ScoreFunctionEnum.BoostFactor;
                    string scoreFunctionKey = functionDict.Keys.FirstOrDefault(x => ScoreFunctionEnum.Find(x) != null);
                    ScoreFunctionEnum scoreFunctionType = ScoreFunctionEnum.Find(scoreFunctionKey);
                    if(string.IsNullOrWhiteSpace(scoreFunctionKey))
                        throw new Exception("Unable to find a score function key.");

                    ScoreFunctionBase functionBase = JsonConvert.DeserializeObject(functionDict[scoreFunctionKey].ToString(), scoreFunctionType.ImplementationType) as ScoreFunctionBase;

                    functionBase.Filter = filter;

                    functionList.Add(functionBase);
                }
            }
            else
            {
                string scoreFunctionKey = scoreDict.Keys.FirstOrDefault(x => ScoreFunctionEnum.Find(x) != null);
                ScoreFunctionEnum scoreFunctionType = ScoreFunctionEnum.Find(scoreFunctionKey);
                if(string.IsNullOrWhiteSpace(scoreFunctionKey))
                    throw new Exception("Unable to find a score function key.");

                ScoreFunctionBase functionBase = JsonConvert.DeserializeObject(scoreDict.GetString(scoreFunctionKey), scoreFunctionType.ImplementationType) as ScoreFunctionBase;

                functionList.Add(functionBase);
            }

            FunctionScoreQueryBase functionScore = null;

            if (scoreDict.ContainsKey(_QUERY))
            {
                IQuery query = JsonConvert.DeserializeObject<IQuery>(scoreDict.GetString(_QUERY));
                functionScore = new FunctionScoreQuery(query, functionList);
            }
            else
            {
                IFilter filter = JsonConvert.DeserializeObject<IFilter>(scoreDict.GetString(_FILTER));
                functionScore = new FunctionScoreFilter(filter, functionList);
            }

            functionScore.Boost = scoreDict.GetDouble(QuerySerializer._BOOST, QuerySerializer._BOOST_DEFAULT);
            functionScore.MaximumBoost = scoreDict.GetDouble(_MAX_BOOST, _MAX_BOOST_DEFAULT);
            functionScore.BoostMode = BoostModeEnum.Find(scoreDict.GetString(_BOOST_MODE, _BOOST_MODE_DEFAULT.ToString()));
            functionScore.ScoreMode = ScoreModeEnum.Find(scoreDict.GetString(_SCORE_MODE, _SCORE_MODE_DEFAULT.ToString()));
            functionScore.QueryName = scoreDict.GetStringOrDefault(QuerySerializer._QUERY_NAME);
            return functionScore;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FunctionScoreQueryBase))
                throw new SerializeTypeException<FunctionScoreQueryBase>();

            FunctionScoreQueryBase functionScore = value as FunctionScoreQueryBase;
            Dictionary<string, object> scoreDict = new Dictionary<string,object>();

            if (functionScore.SearchPiece is IQuery)
                scoreDict.Add(_QUERY, functionScore.SearchPiece as IQuery);
            else if (functionScore.SearchPiece is IFilter)
                scoreDict.Add(_FILTER, functionScore.SearchPiece as IFilter);
            else
                throw new Exception("SearchPiece is not a filter or query.");

            scoreDict.AddObject(QuerySerializer._BOOST, functionScore.Boost, QuerySerializer._BOOST_DEFAULT);

            if (functionScore.ScoreFunctions.Count() == 1 && functionScore.ScoreFunctions.First().Filter == null)
            { 
                ScoreFunctionBase scoreFunctionBase = functionScore.ScoreFunctions.First();
                ScoreFunctionEnum functionEnum = ScoreFunctionEnum.BoostFactor;
                functionEnum = ScoreFunctionEnum.All.FirstOrDefault(x => x.ImplementationType == scoreFunctionBase.GetType());
                scoreDict.Add(functionEnum.ToString(), scoreFunctionBase);
            }
            else if (functionScore.ScoreFunctions.Count() > 0)
            {
                List<Dictionary<string, object>> functionsDictList = new List<Dictionary<string, object>>();
                foreach (ScoreFunctionBase scoreFunction in functionScore.ScoreFunctions)
                {
                    Dictionary<string, object> functionDict = new Dictionary<string, object>();
                    functionDict.AddObject(_FILTER, scoreFunction.Filter);

                    ScoreFunctionEnum functionEnum = ScoreFunctionEnum.BoostFactor;
                    functionEnum = ScoreFunctionEnum.All.FirstOrDefault(x => x.ImplementationType == scoreFunction.GetType());
                    functionDict.Add(functionEnum.ToString(), scoreFunction);
                    functionsDictList.Add(functionDict);
                }

                scoreDict.Add(_FUNCTIONS, functionsDictList);
            }
            else
            {
                throw new Exception("At least one score function is required.");
            }

            scoreDict.AddObject(_MAX_BOOST, functionScore.MaximumBoost, _MAX_BOOST_DEFAULT);
            scoreDict.AddObject(_SCORE_MODE, functionScore.ScoreMode.ToString(), _SCORE_MODE_DEFAULT.ToString());
            scoreDict.AddObject(_BOOST_MODE, functionScore.BoostMode.ToString(), _BOOST_MODE_DEFAULT.ToString());

            Dictionary<string, object> functionScoreDict = new Dictionary<string, object>();
            functionScoreDict.Add(_FUNCTION_SCORE, scoreDict);

            serializer.Serialize(writer, functionScoreDict);
        }
    }
}

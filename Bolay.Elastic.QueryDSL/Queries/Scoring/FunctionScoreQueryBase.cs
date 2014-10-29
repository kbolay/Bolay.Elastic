using Bolay.Elastic.QueryDSL.Queries.Scoring.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-function-score-query.html
    /// </summary>
    [JsonConverter(typeof(FunctionScoreSerializer))]
    public abstract class FunctionScoreQueryBase : QueryBase
    {
        /// <summary>
        /// The filter or query for the function score query.
        /// </summary>
        public ISearchPiece SearchPiece { get; private set; }

        /// <summary>
        /// Boost for the entire query.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// Functions for the function_score query.
        /// </summary>
        public IEnumerable<ScoreFunctionBase> ScoreFunctions { get; set; }

        /// <summary>
        /// Limit the score for any document.
        /// Defaults to the maximum value of a float.
        /// </summary>
        public Double MaximumBoost { get; set; }

        /// <summary>
        /// The method to use for calculating the score for a document.
        /// Defaults to multiply.
        /// </summary>
        public ScoreModeEnum ScoreMode { get; set; }

        /// <summary>
        /// The method to use for determining the boost for a document.
        /// Defaults to multiply.
        /// </summary>
        public BoostModeEnum BoostMode { get; set; }

        internal FunctionScoreQueryBase()
        {
            MaximumBoost = FunctionScoreSerializer._MAX_BOOST_DEFAULT;
            Boost = QuerySerializer._BOOST_DEFAULT;
            ScoreMode = FunctionScoreSerializer._SCORE_MODE_DEFAULT;
            BoostMode = FunctionScoreSerializer._BOOST_MODE_DEFAULT;
        }

        public FunctionScoreQueryBase(ISearchPiece searchPiece, IEnumerable<ScoreFunctionBase> scoreFunctions) : this()
        {
            if (searchPiece == null)
                throw new ArgumentNullException("searchPiece", "FunctionScoreQuery requires either a filter or query.");

            if (scoreFunctions == null || !scoreFunctions.Any())
                throw new ArgumentNullException("scoreFunctions", "FunctionScoreQuery requires at least one score function.");

            SearchPiece = searchPiece;
            ScoreFunctions = scoreFunctions;
        }
    }
}

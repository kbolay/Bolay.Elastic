using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Scoring.Functions
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/query-dsl-function-score-query.html#_script_score
    /// </summary>
    [JsonConverter(typeof(ScriptScoreSerializer))]
    public class ScriptScoreFunction : ScoreFunctionBase
    {
        /// <summary>
        /// Gets the script for the script score function.
        /// </summary>
        public Script Script { get; private set; }

        public ScriptScoreFunction(Script script)
        {
            if (script == null)
                throw new ArgumentNullException("script", "ScriptScoreFunction requires a script.");

            Script = script;
        }
    }
}

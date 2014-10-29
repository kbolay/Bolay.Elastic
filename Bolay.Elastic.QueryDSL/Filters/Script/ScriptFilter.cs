using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Filters.Script
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-script-filter.html
    /// </summary>
    [JsonConverter(typeof(ScriptFilterSerializer))]
    public class ScriptFilter : FilterBase
    {
        /// <summary>
        /// Gets the text of the script used as a filter.
        /// </summary>
        public Elastic.Scripts.Script Script { get; private set; }

        public ScriptFilter(Elastic.Scripts.Script script)
        {
            if (script == null)
                throw new ArgumentNullException("script", "ScriptFilter requires a script.");

            Script = script;
            Cache = ScriptFilterSerializer._CACHE_DEFAULT;
        }
    }
}

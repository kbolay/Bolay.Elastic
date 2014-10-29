using Bolay.Elastic.QueryDSL.Sorting.Field;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Script
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-sort.html#_script_based_sorting
    /// </summary>
    [JsonConverter(typeof(ScriptSortSerializer))]
    public class ScriptSort : ISortClause
    {
        /// <summary>
        /// Gets the script used to produce a value for sorting.
        /// </summary>
        public Elastic.Scripts.Script Script { get; private set; }

        // TODO: Get a real enumeration of the type values allowed.

        /// <summary>
        /// Gets the type of value produced by the script.
        /// Example: number.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets or sets the sort order for the values produced by the script.
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

        // TODO: is mode actually allowed here?

        /// <summary>
        /// Gets or sets the sort mode for arrays of values produced by the script.
        /// </summary>
        public SortModeEnum SortMode { get; set; }

        /// <summary>
        /// Gets or sets if the sort will be reversed.
        /// </summary>
        public bool Reverse { get; set; }

        /// <summary>
        /// Create a script sort without parameters.
        /// </summary>
        /// <param name="script">Sets the text of the script to execute against the documents.</param>
        /// <param name="type">Sets the type of value the script produces.</param>
        public ScriptSort(Elastic.Scripts.Script script, string type)
        {
            if (script == null)
                throw new ArgumentNullException("script", "ScriptSort requires a script.");
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException("type", "ScriptSort requires the type of value produced by the script.");

            Script = script;
            Type = type;
            SortOrder = SortClauseSerializer._ORDER_DEFAULT;
            Reverse = SortClauseSerializer._REVERSE_DEFAULT;
        }
    }
}

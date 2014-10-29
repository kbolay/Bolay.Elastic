using Bolay.Elastic.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.ScriptFields
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-script-fields.html
    /// </summary>
    public class ScriptField
    {
        /// <summary>
        /// Gets the name of the field to hold the value the script produces.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Create a script_field.
        /// </summary>
        /// <param name="field">Sets the field name for the result of the script.</param>
        /// <param name="script">Sets the script to use.</param>
        public ScriptField(string field, Script script)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "ScriptField requires a field.");
            if (script == null)
                throw new ArgumentNullException("script", "ScriptField requires a script.");

            Field = field;
            Script = script;
        }
    }
}

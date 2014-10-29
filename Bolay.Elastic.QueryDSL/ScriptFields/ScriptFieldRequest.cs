using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.ScriptFields
{
    // TODO: rename this class and maybe the entire namespace

    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-script-fields.html
    /// </summary>
    [JsonConverter(typeof(ScriptFieldsSerializer))]
    public class ScriptFieldRequest : ISearchPiece
    {
        /// <summary>
        /// Gets the script fields to request.
        /// </summary>
        public IEnumerable<ScriptField> Fields { get; private set; }

        /// <summary>
        /// Create a script_field value.
        /// </summary>
        /// <param name="fields">The fields to create, and the scripts to create them with.</param>
        public ScriptFieldRequest(IEnumerable<ScriptField> fields)
        {
            if (fields == null || fields.All(x => x == null))
                throw new ArgumentNullException("fields", "ScriptFields requires at least one field.");

            Fields = fields.Where(x => x != null);
        }
    }
}

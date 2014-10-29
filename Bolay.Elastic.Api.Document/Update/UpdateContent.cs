using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Update
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-update.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdateContent
    {
        /// <summary>
        /// Place script in here.
        /// </summary>
        [JsonProperty("script")]
        public string Script { get; private set; }

        /// <summary>
        /// Script parameters belong in here in a json format.
        /// </summary>
        [JsonProperty("params")]
        public Dictionary<string, object> ScriptParameters { get; set; }

        /// <summary>
        /// Default values for script parameters.
        /// </summary>
        [JsonProperty("upsert")]
        public Dictionary<string, object> ScriptParameterUpserts { get; set; }

        /// <summary>
        /// This is ignored if script is included.
        /// </summary>
        [JsonProperty("doc")]
        public Dictionary<string, object> PartialDocument { get; set; }

        /// <summary>
        /// Should the partial document create a full document if the document doesn't not currently exist.
        /// </summary>
        [JsonProperty("doc_as_upsert")]
        public bool EnableDocumentUpsert { get; set; }

        [JsonIgnore]
        public bool IsNull 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Script) && (PartialDocument == null || !PartialDocument.Any()))
                    return true;

                return false;
            }
        }
    }
}

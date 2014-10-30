using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Scripts
{
    [JsonConverter(typeof(ScriptSerializer))]
    public class Script
    {
        public const string SCRIPT = "script";
        internal const string LANGUAGE = "lang";
        internal const string PARAMETERS = "params";

        /// <summary>
        /// Gets the text of the script.
        /// </summary>
        public string ScriptText { get; private set; }

        /// <summary>
        /// Gets or sets the language of the script.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the parameters of the script.
        /// </summary>
        public IEnumerable<ScriptParameter> Parameters { get; set; }

        /// <summary>
        /// Creates a script.
        /// </summary>
        /// <param name="scriptText">Sets the text of the script.</param>
        public Script(string scriptText)
        {
            if (string.IsNullOrWhiteSpace(scriptText))
                throw new ArgumentNullException("scriptText", "Script requires script text.");

            ScriptText = scriptText;
        }
    }
}

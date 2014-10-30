using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Scripts
{
    public static class ScriptExtensions
    {
        public static void Serialize(this Script script, Dictionary<string, object> fieldDict, string alternateScriptKey = null)
        {
            if (script == null || string.IsNullOrWhiteSpace(script.ScriptText))
                return;

            string scriptObjectJson = JsonConvert.SerializeObject(script);
            Dictionary<string, object> scriptDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(scriptObjectJson);

            foreach (KeyValuePair<string, object> kvp in scriptDict)
            {
                if (kvp.Key.Equals(Script.SCRIPT) && !string.IsNullOrEmpty(alternateScriptKey))
                {
                    fieldDict.Add(alternateScriptKey, kvp.Value);
                }
                else
                {
                    fieldDict.Add(kvp.Key, kvp.Value);
                }
            }

        }
    }
}

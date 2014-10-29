using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Scripts
{
    public class ScriptParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ScriptParameter(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "ScriptParameter requires a name.");
            if (value == null)
                throw new ArgumentNullException("value", "ScriptParameter requires a value.");

            Name = name;
            Value = value;
        }
    }

    // TODO : this class should not be available to anyone outside the SDK...
    [JsonConverter(typeof(ScriptParameterCollectionSerializer))]
    public class ScriptParameterCollection : List<ScriptParameter>
    {
        public ScriptParameterCollection()
        { }

        public ScriptParameterCollection(IEnumerable<ScriptParameter> parameters)
        {
            this.AddRange(parameters);
        }
    }
}

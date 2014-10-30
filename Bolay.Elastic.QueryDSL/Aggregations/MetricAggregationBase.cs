using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations
{
    public abstract class MetricAggregationBase : IAggregation
    {
        private const string _FIELD = "field";
        private const string _SCRIPT_VALUES_SORTED = "script_values_sorted";

        private const bool _SCRIPT_VALUES_SORTED_DEFAULT = false;

        /// <summary>
        /// Gets the name of the aggregation.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the field to retrieve the sum value of.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script to use.
        /// </summary>
        public Script Script { get; private set; }

        /// <summary>
        /// Gets or sets the script_values_sorted value.
        /// Defaults to false.
        /// </summary>
        public bool ScriptValuesSorted { get; set; }

        private MetricAggregationBase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All aggregations require a name.");

            Name = name;
        }

        /// <summary>
        /// Create an aggregation base on a field.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="field">Sets the field.</param>
        public MetricAggregationBase(string name, string field)
            : this(name)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "This aggregation requires a field in this constructor.");

            Field = field;
        }

        /// <summary>
        /// Creates an aggregation using field and script.
        /// </summary>
        /// <param name="name">The name of the aggregation.</param>
        /// <param name="field">The field to retrieve values from.</param>
        /// <param name="script">The script that acts on the values.</param>
        public MetricAggregationBase(string name, string field, Script script)
            : this(name, field)
        {
            if (script == null)
                throw new ArgumentNullException("script", "This aggregation requires a script in this constructor.");

            Script = script;
        }

        /// <summary>
        /// Creates an aggregation based on a script.
        /// </summary>
        /// <param name="name">Sets the name of the aggregation.</param>
        /// <param name="script">Sets the script.</param>
        public MetricAggregationBase(string name, Script script)
            : this(name)
        {
            if (script == null)
                throw new ArgumentNullException("script", "This aggregation requires a script in this constructor.");

            Script = script;
        }

        internal Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.AddObject(_FIELD, this.Field);
            this.Script.Serialize(dict);
            dict.AddObject(_SCRIPT_VALUES_SORTED, this.ScriptValuesSorted, _SCRIPT_VALUES_SORTED_DEFAULT);

            return dict;
        }

        internal static T Deserialize<T>(string name, Dictionary<string, object> fieldDict) where T : MetricAggregationBase
        {
            string field = fieldDict.GetStringOrDefault(_FIELD);
            Script script = fieldDict.DeserializeObject<Script>();

            List<object> constructorArgs = new List<object>() { name };
            if (!string.IsNullOrWhiteSpace(field))
                constructorArgs.Add(field);
            if (script != null)
                constructorArgs.Add(script);

            if(constructorArgs.Count == 1)
                throw new RequiredPropertyMissingException(_FIELD + "/" + Script.SCRIPT);

            MetricAggregationBase agg = Activator.CreateInstance(typeof(T), constructorArgs.ToArray()) as MetricAggregationBase;
            agg.ScriptValuesSorted = fieldDict.GetBool(_SCRIPT_VALUES_SORTED, _SCRIPT_VALUES_SORTED_DEFAULT);

            return (T)agg;
        }
    }
}

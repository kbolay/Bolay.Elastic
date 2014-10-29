using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Statistics
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-statistical-facet.html
    /// </summary>
    [JsonConverter(typeof(StatisticsFacetSerializer))]
    public class StatisticsFacet : IFacet
    {
        /// <summary>
        /// Gets the facet name.
        /// </summary>
        public string FacetName { get; private set; }

        /// <summary>
        /// Gets the field to base the statistical analysis on.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets the script to create statistical analysis.
        /// </summary>
        public Script Script { get; private set; }

        ///// <summary>
        ///// Gets the script parameters.
        ///// </summary>
        //public IEnumerable<ScriptParameter> ScriptParameters { get; private set; }

        internal StatisticsFacet(string facetName)
        {
            if (string.IsNullOrEmpty(facetName))
                throw new ArgumentNullException("facetName", "StatisticalFacet requires a facet name.");

            FacetName = facetName;
        }

        /// <summary>
        /// Creates a statistical facet using a specific field.
        /// </summary>
        /// <param name="facetName">Sets the name of the facet.</param>
        /// <param name="field">Sets the field to get statistics from.</param>
        public StatisticsFacet(string facetName, string field)
            : this(facetName)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "StatisticalFacet requires a field in this constructor.");

            Field = field;
        }

        /// <summary>
        /// Creates a statistical facet using a script and optionally script parameters.
        /// </summary>
        /// <param name="facetName">Sets the facet name.</param>
        /// <param name="script">Sets the script for the facet.</param>
        /// <param name="scriptParameters">Sets the parameters for the script.</param>
        public StatisticsFacet(string facetName, Script script)
            : this(facetName)
        {
            if (script == null)
                throw new ArgumentNullException("script", "StatisticalFacet requires a script in this constructor.");

            Script = script;
        }
    }
}

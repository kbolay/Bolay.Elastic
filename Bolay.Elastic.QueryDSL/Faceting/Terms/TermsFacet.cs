using Bolay.Elastic.Models;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting.Terms
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-facets-terms-facet.html
    /// </summary>
    [JsonConverter(typeof(TermsSerializer))]
    public class TermsFacet : FacetBase
    {
        private int _ShardSize { get; set; }

        /// <summary>
        /// Gets or sets the number of facet terms to return retrieve from each shard.
        /// Defaults to size value.
        /// </summary>
        public int ShardSize 
        {
            get { return _ShardSize; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("ShardSize", "ShardSize must be greater than or equal to zero.");

                _ShardSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the method of ordering facet terms.
        /// Defaults to count.
        /// </summary>
        public OrderingEnum Order { get; set; }

        /// <summary>
        /// Gets or sets whether to retrieve all terms for the facet.
        /// Defaults to false.
        /// </summary>
        public bool GetAllTerms { get; set; }

        /// <summary>
        /// Gets or sets the terms to exclude.
        /// Defaults to null.
        /// </summary>
        public IEnumerable<object> ExcludeTerms { get; set; }

        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// Defaults to null.
        /// </summary>
        public string RegexPattern { get; set; }

        /// <summary>
        /// Gets or sets the regular expression flags.
        /// Defaults to DOTALL.
        /// </summary>
        public IEnumerable<RegexFlagEnum> RegexFlags { get; set; }

        /// <summary>
        /// Gets or sets a script that can be used to modify the value or change if it is included.
        /// Defaults to null.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// Gets the field(s) to retrieve facet terms from.
        /// </summary>
        public IEnumerable<string> Fields { get; private set; }

        /// <summary>
        /// Gets the script_field value.
        /// </summary>
        public string ScriptField { get; private set; }

        /// <summary>
        /// Gets or sets the script parameters.
        /// </summary>
        public IEnumerable<ScriptParameter> ScriptParameters { get; set; }

        /// <summary>
        /// Gets or sets the script language.
        /// </summary>
        public string ScriptLanguage { get; set; }

        private TermsFacet(string facetName)
            : base(facetName)
        {
            ShardSize = FacetSerializer._SIZE_DEFAULT;
            Order = TermsSerializer._ORDER_DEFAULT;
            GetAllTerms = TermsSerializer._ALL_TERMS_DEFAULT;
            RegexFlags = TermsSerializer._REGEX_FLAGS_DEFAULT;
        }

        /// <summary>
        /// Create a terms facet with a script field.
        /// </summary>
        /// <param name="facetName">The name of the facet request.</param>
        /// <param name="scriptField">The field to retrieve facet terms from.</param>
        /// <param name="isScript">Set this to true using a script field value.</param>
        public TermsFacet(string facetName, string scriptField)
            : this(facetName)
        {
            if (string.IsNullOrWhiteSpace(scriptField))
                throw new ArgumentNullException("scriptField", "TermsFacet requires a script field in this constructor.");

            ScriptField = scriptField;
        }

        /// <summary>
        /// Create a terms facet with one or more fields to collect terms from.
        /// </summary>
        /// <param name="facetName">The name of the facet request.</param>
        /// <param name="fields">The fields to retrieve facet terms from.</param>
        public TermsFacet(string facetName, IEnumerable<string> fields)
            : this(facetName)
        {
            if (fields == null || fields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("fields", "TermsFacet requires at least one field in this constructor");

            Fields = fields.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis
{
    [JsonConverter(typeof(FuzzyLikeThisSerializer))]
    public abstract class FuzzyLikeThisBase : QueryBase
    {
        private IEnumerable<string> _Fields { get; set; }

        /// <summary>
        /// The fields to search against.
        /// Defaults to _all.
        /// </summary>
        public IEnumerable<string> Fields 
        {
            get 
            {
                if (_Fields == null || !_Fields.Any())
                    return new List<string> { "_all" };

                return _Fields;
            }
            internal set
            {
                if (value != null && value.Any())
                    _Fields = value;
            }
        }

        /// <summary>
        /// The like_text value.
        /// </summary>
        public string Query { get; internal set; }

        /// <summary>
        /// Ignore the frequency of terms.
        /// </summary>
        public bool IgnoreTermFrequency { get; set; }

        /// <summary>
        /// The maximum number of terms to be included in any generated query.
        /// Defaults to 25.
        /// </summary>
        public int MaximumQueryTerms { get; set; }

        /// <summary>
        /// Minimum similarity between the variants of the terms.
        /// Defaults to 0.5
        /// </summary>
        public Double Fuzziness { get; set; }

        /// <summary>
        /// The required length of common prefixes on the variant terms.
        /// </summary>
        public int PrefixLength { get; set; }

        /// <summary>
        /// The boost value for the query.
        /// Defaults to 1.0.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// Populate this if the desire is to analyze the search in a different way than is configured.
        /// </summary>
        public string Analyzer { get; set; }

        internal FuzzyLikeThisBase()
        {
            Fuzziness = FuzzyLikeThisSerializer._FUZZINESS_DEFAULT;
            MaximumQueryTerms = FuzzyLikeThisSerializer._MAX_QUERY_TERMS_DEFAULT;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }

        /// <summary>
        /// Create a fuzzy like this query.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="query"></param>
        public FuzzyLikeThisBase(IEnumerable<string> fields, string query) : this()
        {
            if (fields == null || fields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("field", "FuzzyLikeThis requires field(s) to search against.");
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "FuzzyLikeThis requires query for like_this field.");

            Fields = fields.Where(x => !string.IsNullOrWhiteSpace(x));
            Query = query;
        }
    }
}

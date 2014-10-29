using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.MoreLikeThis
{
    [JsonConverter(typeof(MoreLikeThisSerializer))]
    public abstract class MoreLikeThisBase : QueryBase
    {
        /// <summary>
        /// The fields to search against.
        /// Defaults to _all.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// The like_text value.
        /// </summary>
        public string Query { get; internal set; }

        /// <summary>
        /// The percentage of terms to match on. Defaults to 0.3, 30%.
        /// </summary>
        public Double PercentageTermsToMatch { get; set; }

        /// <summary>
        /// The lower threshold for ignoring terms in the soure document.
        /// </summary>
        public int MinimumTermFrequency { get; set; }

        /// <summary>
        /// The maximum number of terms to be included in any generated query.
        /// Defaults to 25.
        /// </summary>
        public int MaximumQueryTerms { get; set; }

        /// <summary>
        /// A collection of words to be ignored.
        /// </summary>
        public IEnumerable<string> StopWords { get; set; }

        /// <summary>
        /// The minimum number of documents a term must exist in to be included in the query.
        /// </summary>
        public int MinimumDocumentFrequency { get; set; }

        /// <summary>
        /// The maximum number of documents a term can appear in before being ignored.
        /// </summary>
        public int? MaximumDocumentFrequency { get; set; }

        /// <summary>
        /// The minimum number of characters in a word before it is ignored.
        /// </summary>
        public int MinimumWordLength { get; set; }

        /// <summary>
        /// The maximum number of characters in a word before it is ignored.
        /// </summary>
        public int MaximumWordLength { get; set; }

        /// <summary>
        /// The boost factor to use with boosting terms.
        /// </summary>
        public int BoostTerms { get; set; }

        /// <summary>
        /// The boost value for the query.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// The analyzer used to analyze the text.
        /// Defaults to the analyzer associated with the field being searched.
        /// </summary>
        public string Analyzer { get; set; }

        internal MoreLikeThisBase()
        {
            PercentageTermsToMatch = MoreLikeThisSerializer._PERCENTAGE_TERMS_TO_MATCH_DEFAULT;
            MinimumTermFrequency = MoreLikeThisSerializer._MINIMUM_TERM_FREQUENCY_DEFAULT;
            MaximumQueryTerms = MoreLikeThisSerializer._MAXIMUM_QUERY_TERMS_DEFAULT;
            MinimumDocumentFrequency = MoreLikeThisSerializer._MINIMUM_DOCUMENT_FREQUENCY_DEFAULT;
            MinimumWordLength = MoreLikeThisSerializer._MINIMUM_WORD_LENGTH_DEFAULT;
            MaximumWordLength = MoreLikeThisSerializer._MAXIMUM_WORD_LENGTH_DEFAULT;
            BoostTerms = MoreLikeThisSerializer._BOOST_TERMS_DEFAULT;
            Boost = QuerySerializer._BOOST_DEFAULT;
        }

        public MoreLikeThisBase(string query) : this()
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "MoreLikeThis queries expect query text.");

            Query = query;
        }

        public MoreLikeThisBase(IEnumerable<string> fields, string query) : this(query)
        {
            if (fields == null || fields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("field", "MoreLikeThis queries expect field(s) to search against.");           

            Fields = fields.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    [JsonConverter(typeof(MatchSerializer))]
    public abstract class MatchQueryBase : QueryBase
    {
        private OperatorEnum _Operator { get; set; }
        private RewriteMethodsEnum _RewriteMethod { get; set; }
        private ZeroTermsEnum _ZeroTerm { get; set; }
        private MatchQueryTypeEnum _Type { get; set; }

        public string Type
        {
            get
            {
                if (_Type != null)
                    return _Type.ToString();

                return MatchQueryTypeEnum.Boolean.ToString();
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    MatchQueryTypeEnum temp = MatchQueryTypeEnum.Boolean;
                    temp = MatchQueryTypeEnum.Find(value);

                    if (temp != null)
                        _Type = temp;
                    else
                        _Type = MatchQueryTypeEnum.Boolean;
                }
                else
                    _Type = MatchQueryTypeEnum.Boolean;
            }
        }
        public string Field { get; set; }
        public string Query { get; set; }
        public OperatorEnum Operator { get; set; }
        public int MinimumShouldMatch { get; set; }
        public string Analyzer { get; set; }
        public Double Fuzziness { get; set; }
        public int PrefixLength { get; set; }
        public int? MaximumExpansions { get; set; }
        public RewriteMethodsEnum RewriteMethod { get; set; }
        public ZeroTermsEnum ZeroTerm { get; set; }
        public Double CutOffFrequency { get; set; }
        public bool IsLenient { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        internal MatchQueryBase() 
        {
            Operator = MatchSerializer._OPERATOR_DEFAULT;
            MinimumShouldMatch = MatchSerializer._MINIMUM_SHOULD_MATCH_DEFAULT;
            Fuzziness = MatchSerializer._FUZZINESS_DEFAULT;
            PrefixLength = MatchSerializer._PREFIX_LENGTH_DEFAULT;
            ZeroTerm = MatchSerializer._ZERO_TERMS_QUERY_DEFAULT;
            CutOffFrequency = MatchSerializer._CUTOFF_FREQUENCY_DEFAULT;
            IsLenient = MatchSerializer._LENIENT_DEFAULT;
        }

        /// <summary>
        /// Constructor for serialization of inheritor classes.
        /// </summary>
        /// <param name="matchType"></param>
        protected MatchQueryBase(MatchQueryTypeEnum matchType):this()
        {
            if (matchType == null)
                throw new ArgumentNullException("matchType", "The type of match query must be specified.");

            _Type = matchType;
        }

        /// <summary>
        /// Constructor for multi_match queries.
        /// </summary>
        /// <param name="matchType"></param>
        /// <param name="query"></param>
        protected MatchQueryBase(MatchQueryTypeEnum matchType, string query):this()
        {
            if (matchType == null)
                throw new ArgumentNullException("matchType", "The type of match query must be specified.");
            if (matchType != MatchQueryTypeEnum.Multi)
                throw new ArgumentOutOfRangeException("matchType", "Non multi_match queries must use the constructor with a field parameter.");
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "The query must be populated.");

            _Type = matchType;
            Query = query;
        }

        /// <summary>
        /// Constructor for boolean, phrase, and phrase prefix match types.
        /// </summary>
        /// <param name="matchType">The type of match query to use. Do not use multi_match in this constructor.</param>
        /// <param name="field">The field to search in.</param>
        /// <param name="query">The values to search for in the field.</param>
        protected MatchQueryBase(MatchQueryTypeEnum matchType, string field, string query):this()
        {
            if (matchType == null)
                throw new ArgumentNullException("matchType", "The type of match query must be specified.");
            if(matchType == MatchQueryTypeEnum.Multi)
                throw new ArgumentOutOfRangeException("matchType", "The multi_match query must use the constructor allowing multiple fields.");
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "The field to query against must be specified.");
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query", "The query must be populated.");

            _Type = matchType;
            Field = field;
            Query = query;
        }
    }
}

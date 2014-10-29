using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Match
{
    //[JsonConverter(typeof(MultiMatchSerializer))]
    public class MultiMatchQuery : MatchQueryBase
    {
        private const string _BOOST_FIELD_DELIMITER = "^";

        private IEnumerable<BoostedField> _Fields { get; set; }

        /// <summary>
        /// The fields, boost allowed, to search against.
        /// </summary>
        public IEnumerable<string> Fields 
        { 
            get 
            {
                if (_Fields != null && _Fields.Any())
                    return _Fields.Select(x => x.ToString());

                return null;
            } 
            set 
            {
                _Fields = ParseFields(value);
            } 
        }

        /// <summary>
        /// This tells ElasticSearch to build a dis_max query instead of a boolean query.
        /// </summary>
        public bool UseDisMaxQuery { get; set; }

        /// <summary>
        /// Used to balance scores between higher and lower scoring fields.
        /// Only applied when use_dis_max is true.
        /// </summary>
        public Double TieBreakerMultiplier { get; set; }

        internal MultiMatchQuery() : base(MatchQueryTypeEnum.Multi) { }

        /// <summary>
        /// Create a multi_match query.
        /// </summary>
        /// <param name="fields">The string form of the fields to search against.</param>
        /// <param name="query">The query used against the supplied fields.</param>
        public MultiMatchQuery(IEnumerable<string> fields, string query)
            : this(ParseFields(fields), query)
        { }

        /// <summary>
        /// Create a multi_match query. 
        /// </summary>
        /// <param name="boostedFields">Fields that a boost value can be applied to.</param>
        /// <param name="query">The values to search for.</param>
        public MultiMatchQuery(IEnumerable<BoostedField> boostedFields, string query)
            : base(MatchQueryTypeEnum.Multi, query)
        {
            if (boostedFields == null || !boostedFields.Any())
                throw new ArgumentNullException("boostedFields", "MultiMatch query requires a collection of boosted fields.");

            _Fields = boostedFields;
        }

        private static IEnumerable<BoostedField> ParseFields(IEnumerable<string> fields)
        {
            if (fields == null || !fields.Any())
                throw new ArgumentNullException("fields", "MultiMatch must have at least one field specified.");

            List<BoostedField> boostedFields = new List<BoostedField>();
            foreach (string field in fields.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                string[] splitField = field.Split(new string[] { _BOOST_FIELD_DELIMITER }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (splitField.Length == 1)
                {
                    boostedFields.Add(new BoostedField(splitField[0]));
                }
                else if (splitField.Length == 2)
                {
                    Double boost = 0;
                    if (!Double.TryParse(splitField[1], out boost))
                    {
                        throw new Exception("Malformed BoostedField in MultiMatch query, the boost field was not a double.");
                    }

                    boostedFields.Add(new BoostedField(splitField[0], boost, _BOOST_FIELD_DELIMITER));
                }
                else
                {
                    throw new Exception("Malformed BoostedField in MultiMatch query. Multiple delimiters found.");
                }
            }

            return boostedFields;
        }
    }
}

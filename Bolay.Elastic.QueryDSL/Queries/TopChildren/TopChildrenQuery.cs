using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.TopChildren
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-top-children-query.html
    /// </summary>
    [JsonConverter(typeof(TopChildrenSerializer))]
    public class TopChildrenQuery : QueryBase
    {
        private int _Factor { get; set; }
        private int _IncrementalFactor { get; set; }

        /// <summary>
        /// The type of child document to query on.
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// The query to run against the child documents.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// The scoring method to use.
        /// Defaults to max.
        /// </summary>
        public ScoreTypeEnum Score { get; set; }

        /// <summary>
        /// Since parents can have multiple children this value is multiplied against the from value for the search.
        /// This internally retrieves a lot of children documents in hopes that the unique parent documents meet the 
        /// size value for the search.
        /// Must be greater than zero.
        /// </summary>
        public int Factor
        {
            get { return _Factor; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Factor", "The Factor value must be greater than zero.");

                _Factor = value;
            }
        }

        /// <summary>
        /// If the first internal search does not return the number of parent documents specified by the size value
        /// then another child search is done using this value as the multiplier.
        /// 
        /// </summary>
        public int IncrementalFactor
        {
            get { return _IncrementalFactor; }
            set 
            { 
                if(value <= 0)
                    throw new ArgumentOutOfRangeException("IncrementalFactor", "The IncrementalFactor value must be greater than zero.");

                _IncrementalFactor = value;
            }
        }

        /// <summary>
        /// Allow a facets request on this property.
        /// </summary>
        public string Scope { get; set; }

        public TopChildrenQuery(string documentType, IQuery query)
        {
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "TopChildrenQuery requires a documentType.");
            if (query == null)
                throw new ArgumentNullException("query", "TopChildrenQuery requires a query.");

            DocumentType = documentType;
            Query = query;

            Score = TopChildrenSerializer._SCORE_DEFAULT;
            Factor = TopChildrenSerializer._FACTOR_DEFAULT;
            IncrementalFactor = TopChildrenSerializer._INCREMENTAL_FACTOR_DEFAULT;
        }
    }
}

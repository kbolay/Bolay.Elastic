using Bolay.Elastic.QueryDSL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Rescoring
{
    public class RescoreQuery
    {
        /// <summary>
        /// Gets or sets the number of documents to use this query against on each shard.
        /// Defaults to the from and size values of the search.
        /// </summary>
        public int? WindowSize { get; set; }

        /// <summary>
        /// Gets or sets the weight value to be applied to the results of the search query.
        /// Defaults to 1.0.
        /// </summary>
        public Double QueryWeight { get; set; }

        /// <summary>
        /// Gets or sets the weight value to be applied to the results of the rescore query.
        /// Defaults to 1.0.
        /// </summary>
        public Double RescoreQueryWeight { get; set; }        

        /// <summary>
        /// Gets or sets the score mode for combining the rescore and original score for a document.
        /// Defaults to total.
        /// </summary>
        public ScoreModeEnum ScoreMode { get; set; }

        /// <summary>
        /// Gets the query that will act as the rescore score.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Create a rescore query.
        /// </summary>
        /// <param name="query">The query to run for the purpose of rescoring.</param>
        public RescoreQuery(IQuery query)
        {
            if (query == null)
                throw new ArgumentNullException("query", "RescoreQuery requires a query.");

            Query = query;
            ScoreMode = RescoreSerializer._SCORE_MODE_DEFAULT;
            QueryWeight = RescoreSerializer._QUERY_WEIGHT_DEFAULT;
            RescoreQueryWeight = RescoreSerializer._RESCORE_QUERY_WEIGHT_DEFAULT;
        }
    }
}

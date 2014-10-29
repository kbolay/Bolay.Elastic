using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Nested
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-nested-query.html
    /// </summary>
    [JsonConverter(typeof(NestedSerializer))]
    public class NestedQuery : QueryBase
    {
        /// <summary>
        /// Gets the path to the nested object.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets or sets the scoring mode affects the matching inner child documents combining score with the parent documents.
        /// Defaults to average.
        /// </summary>
        public ScoreTypeEnum ScoreMode { get; set; }

        /// <summary>
        /// Gets the query to use when searching against the nested documents.
        /// </summary>
        public IQuery Query { get; private set; }

        /// <summary>
        /// Gets or sets whether to perform the block join or not.
        /// Defaults to true.
        /// </summary>
        public bool Join { get; set; }

        internal NestedQuery()
        {
            ScoreMode = NestedSerializer._SCORE_MODE_DEFAULT;
            Join = NestedSerializer._JOIN_DEFAULT;
        }

        public NestedQuery(string path, IQuery query)
            : this()
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path", "Nested query requires a path to the nested object.");
            if (query == null)
                throw new ArgumentNullException("query", "Nested query requires a query.");

            Path = path;
            Query = query;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.SourceFiltering
{
    // TODO: Test with source filtering so i know what the return format looks like.
    // Need to get a 1.0 environment.

    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-source-filtering.html
    /// </summary>
    [JsonConverter(typeof(SourceFilterSerializer))]
    public class SourceFilter : ISearchPiece
    {
        /// <summary>
        /// Gets whether the search will disable source retrieval.
        /// </summary>
        public bool DisableSourceRetrieval { get; private set; }

        /// <summary>
        /// Gets the fields to include in the return documents.
        /// </summary>
        public IEnumerable<string> IncludeFields { get; private set; }

        /// <summary>
        /// Gets the fields to exclude in the return documents.
        /// </summary>
        public IEnumerable<string> ExcludeFields { get; private set; }

        /// <summary>
        /// Create a source filter that can disable source retrieval.
        /// This constructor sets the DisableSourceRetrieval to true.
        /// </summary>
        public SourceFilter()
        {
            DisableSourceRetrieval = true;
        }

        /// <summary>
        /// Create a source filter to include one field.
        /// </summary>
        /// <param name="includeField">sets the field to include.</param>
        public SourceFilter(string includeField)
        {
            if (string.IsNullOrWhiteSpace(includeField))
                throw new ArgumentNullException("includeField", "SourceFilter requires an include field for this constructor.");

            IncludeFields = new List<string>() { includeField };
        }

        /// <summary>
        /// Creates a source filter to include multiple fields.
        /// </summary>
        /// <param name="includeFields"></param>
        public SourceFilter(IEnumerable<string> includeFields)
        {
            if (includeFields == null || includeFields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("includeFields", "SourceFilter requires at least one field to include in this constructor.");

            IncludeFields = includeFields;
        }

        /// <summary>
        /// Create a source filter that include and exclude spefic fields.
        /// </summary>
        /// <param name="includeFields">Sets the fields to include.</param>
        /// <param name="excludeFields">Sets the fields to exclude.</param>
        public SourceFilter(IEnumerable<string> includeFields, IEnumerable<string> excludeFields)
        {
            if (includeFields == null || includeFields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("includeFields", "SourceFilter requires at least one field to include in this constructor.");
            if(excludeFields == null || excludeFields.All(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException("excludeFields", "SourceFilter requires at least one field to exclude in this constructor.");

            IncludeFields = includeFields.Where(x => !string.IsNullOrWhiteSpace(x));
            ExcludeFields = excludeFields.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}

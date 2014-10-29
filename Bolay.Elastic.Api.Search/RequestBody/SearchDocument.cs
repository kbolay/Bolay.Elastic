using Bolay.Elastic.QueryDSL.Aggregations;
using Bolay.Elastic.QueryDSL.Faceting;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Highlighting;
using Bolay.Elastic.QueryDSL.Queries;
using Bolay.Elastic.QueryDSL.Rescoring;
using Bolay.Elastic.QueryDSL.ScriptFields;
using Bolay.Elastic.QueryDSL.Sorting;
using Bolay.Elastic.QueryDSL.SourceFiltering;
using Bolay.Elastic.QueryDSL.Suggesters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Search.RequestBody
{
    public class SearchDocument
    {
        /// <summary>
        /// Gets or sets the offset for the first document to be returned.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-from-size.html
        /// Defaults to 0.
        /// </summary>
        public Int64 From { get; set; }

        /// <summary>
        /// Gets or sets the number of documents to retrieve.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-from-size.html
        /// Defaults to 10.
        /// </summary>
        public Int64 Size { get; set; }

        /// <summary>
        /// Gets or sets the sort clauses used to sort documents matching the query and filters.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-sort.html
        /// </summary>
        public IEnumerable<ISortClause> SortClauses { get; set; }

        /// <summary>
        /// Gets or sets the query to use for finding documents and applying relevancy score.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-queries.html
        /// </summary>
        public IQuery Query { get; set; }

        /// <summary>
        /// Gets or sets the filter to use for finding documents without applying relevancy score.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-filters.html
        /// </summary>
        public IFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets a final filter. Best used when dealing with an expensive filter. 
        /// This filter only affects the documents returned, not any facet results.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-post-filter.html
        /// </summary>
        public IFilter PostFilter { get; set; }

        /// <summary>
        /// Gets or sets the _source value. Controls how the source field is returned for every hit.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-source-filtering.html
        /// </summary>
        public SourceFilter SourceFilter { get; set; }

        /// <summary>
        /// Gets or sets the fields to retrieve based on scripts.
        /// </summary>
        public ScriptFieldRequest ScriptField { get; set; }

        /// <summary>
        /// Gets or sets the specific stored fields to load for each document matching the rest of the search document.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-fields.html
        /// TODO: find a way to deserialize this.... please use source filter.
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Gets or sets the fields to return fielddata for.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-fielddata-fields.html
        /// </summary>
        public IEnumerable<string> FieldDataFields { get; set; }

        /// <summary>
        /// Gets or sets the specification for highlighting seach results.
        /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-highlighting.html
        /// </summary>        
        public Highlighter Highlighter { get; set; }

        /// <summary>
        /// Gets or sets the rescore algorithm.
        /// </summary>
        public Rescore RescoreQuery { get; set; }

        /// <summary>
        /// Gets or sets the search type used.
        /// Defaults to query_then_fetch.
        /// </summary>
        public SearchTypeEnum SearchType { get; set; }

        /// <summary>
        /// Gets or sets whether an explanation is provided for the matching documents.
        /// </summary>
        public bool Explain { get; set; }

        /// <summary>
        /// Gets or sets whether the documents returned will contain their version.
        /// </summary>
        public bool Version { get; set; }

        /// <summary>
        /// Gets or sets the minimum score a document must have to be returned.
        /// </summary>
        public Double MinimumScore { get; set; }

        /// <summary>
        /// Gets or sets the suggest for a did you mean like result set.
        /// </summary>
        public Suggest Suggest { get; set; }

        /// <summary>
        /// Gets or sets the facets for the request.
        /// </summary>
        public Facets Facets { get; set; }

        /// <summary>
        /// Gets or sets the aggregations for the request.
        /// </summary>
        public Aggregations Aggregations { get; set; }

        //TODO: Testing involving fields, source filtering and other things that affect/add to the way source is returned.
        // currently script
    }
}

using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Api.ShardPreference;
using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Get
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
    /// </summary>
    public class GetDocumentRequest : DocumentRequestBase
    {
        private const string _REALTIME_KEY = "realtime";
        private const string _FIELDS_KEY = "fields";
        private const string _ROUTING_KEY = "routing";
        private const string _REFRESH_KEY = "refresh";
        private const string _PREFERENCE_KEY = "preference";

        private const string _REALTIME_VALUE = "false";
        private const string _DOCUMENT_TYPE_DEFAULT = "_all";
        private const string _EXCLUDE_METADATA_VALUE = "_source";
        private const string _REFRESH_VALUE = "true";

        /// <summary>
        /// By default, the get API is realtime, and is not affected by the 
        /// refresh rate of the index (when data will become visible for search). 
        /// In order to disable realtime GET, one can either set realtime parameter 
        /// to false, or globally default it to by setting the action.get.realtime 
        /// to false in the node configuration.
        /// </summary>
        public bool DisableRealTime { get; set; }

        /// <summary>
        /// The fields of the document to return.
        /// Example: ['prop1', 'prop2', 'prop3.prop1']
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Exclude metadata that normally surrounds the _source document.
        /// </summary>
        public bool ExcludeMetaData { get; set; }

        /// <summary>
        /// If the document was indexed using routing, then this property 
        /// should be populated with the same value this document was indexed under.
        /// </summary>
        public string Routing { get; set; }

        /// <summary>
        /// The request will run against specific shards if this property is set.
        /// </summary>
        public ShardPreferenceBase ShardPreference { get; set; }

        /// <summary>
        /// Refresh the shard before the search, do not set this value to true on accident.
        /// Please read the documentation before utilizing this option.
        /// </summary>
        public bool RefreshBeforeSearch { get; set; }

        /// <summary>
        /// Create a GET document request.
        /// </summary>
        /// <param name="index">The index or alias of the ES cluster containing the document. Required.</param>
        /// <param name="documentType">
        /// The type of document. If _all is used the first document 
        /// matching the documentId will be returned, without regard to type.
        /// </param>
        /// <param name="documentId">The _id of the ES document. Required.</param>       
        public GetDocumentRequest(string index, string documentType, string documentId)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "The index value must be populated.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "The documentId value must be populated.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "The documentType value must be populated.");

            Index = index;
            DocumentId = documentId;
            DocumentType = documentType;
        }

        /// <summary>
        /// Builds the path and query string of the request, using the BuildQueryString method,
        /// and adds this to the cluster uri.
        /// </summary>
        /// <param name="uriProvider">The uri of the elastic cluster.</param>
        /// <returns></returns>
        public override Uri BuildUri(IElasticUriProvider uriProvider)
        {
            StringBuilder pathBuilder = new StringBuilder();
            pathBuilder.Append(Index);
            pathBuilder.Append("/");
            pathBuilder.Append(DocumentType);
            pathBuilder.Append("/");
            pathBuilder.Append(DocumentId);
            
            if(ExcludeMetaData)
            {
                pathBuilder.Append("/");
                pathBuilder.Append(_EXCLUDE_METADATA_VALUE);
            }

            pathBuilder.Append(BuildQueryString());

            return new Uri(uriProvider.ClusterUri, pathBuilder.ToString());
        }

        /// <summary>
        /// Builds the query string of the request. 
        /// This method was left public to allow for unit testing.
        /// </summary>
        /// <returns></returns>
        public override string BuildQueryString()
        {
            StringBuilder builder = new StringBuilder();
            if (DisableRealTime) 
            {
                builder = HttpRequest.AddToQueryString(builder, _REALTIME_KEY, _REALTIME_VALUE); 
            }

            if (Fields != null && Fields.Any()) 
            {
                builder = HttpRequest.AddToQueryString(builder, _FIELDS_KEY, string.Join(",", Fields)); 
            }

            if (!string.IsNullOrWhiteSpace(Routing)) 
            {
                builder = HttpRequest.AddToQueryString(builder, _ROUTING_KEY, Routing);
            }
            
            if (ShardPreference != null && !string.IsNullOrWhiteSpace(ShardPreference.ToString())) 
            {
                builder = HttpRequest.AddToQueryString(builder, _PREFERENCE_KEY, ShardPreference.ToString());
            }

            if (RefreshBeforeSearch) 
            {
                builder = HttpRequest.AddToQueryString(builder, _REFRESH_KEY, _REFRESH_VALUE);
            }

            if (builder.Length == 0)
                return null;

            return builder.ToString();
        }
    }
}

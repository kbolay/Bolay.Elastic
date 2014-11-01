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
        internal const string REALTIME_KEY = "realtime";
        internal const string FIELDS_KEY = "fields";
        internal const string ROUTING_KEY = "routing";
        internal const string REFRESH_KEY = "refresh";
        internal const string SHARD_PREFERENCE_KEY = "preference";

        internal const string REALTIME_DEFAULT = "false";
        internal const string DOCUMENT_TYPE_DEFAULT = "_all";
        internal const string EXCLUDE_METADATA_DEFAULT = "_source";
        internal const string REFRESH_DEFAULT = "true";

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
    }
}

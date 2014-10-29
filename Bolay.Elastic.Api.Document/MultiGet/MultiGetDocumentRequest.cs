using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.MultiGet
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
    /// </summary>
    public class MultiGetDocumentRequest : DocumentRequestBase
    {
        private const string _MULTI_GET_VALUE = "_mget";
        private const string _ROUTING_KEY = "routing";

        public MultiGetRequestContent Content { get; set; }
        public string Routing { get; set; }

        public MultiGetDocumentRequest(MultiGetRequestContent content, string index = null, string documentType = null)
        {
            if (content == null || content.IsNull)
            {
                throw new ArgumentNullException("content", "Multi Get Request requires either the documents or documentids property in MultiGetContent to be populated.");
            }

            if (content.Documents != null && content.Documents.Any())
            {
                if (!content.AllIndexesSpecifiedInDocuments && !content.NoIndexesSpecifiedInDocuments)
                {
                    throw new ArgumentNullException("index", "Either all MultiGetRequestedDocuments must have their index value populated or none may have their index populated.");
                }
                else if (content.NoIndexesSpecifiedInDocuments && string.IsNullOrWhiteSpace(index))
                {
                    throw new ArgumentNullException("index", "If the provided MultiGetRequestedDocuments have no index value the index must be provided to this constructor.");
                }
                else if(content.AllIndexesSpecifiedInDocuments && !string.IsNullOrWhiteSpace(index))
                {
                    throw new ArgumentException("index", "If the provided MultiGetRequestedDocuments all have an index value the index should not be provided to this constructor.");
                }                
            }
            else if (content.DocumentIds != null && content.DocumentIds.Any())
            {
                if (string.IsNullOrWhiteSpace(index))
                {
                    throw new ArgumentNullException("index", "MultiGetDocumentRequest requires an index when doing a multi get using document ids.");
                }

                if (string.IsNullOrWhiteSpace(documentType))
                {
                    throw new ArgumentNullException("documentType", "MultiGetDocumentRequest requires the document type when doing a multi get using document ids.");
                }
            }
            else
            {
                throw new ArgumentNullException("content", "MultiGetRequestContent must have the Documents or DocumentIds value populated.");
            }

            Index = index;
            DocumentType = documentType;
            Content = content;
        }

        public override Uri BuildUri(IElasticUriProvider clusterUriProvider)
        {
            StringBuilder pathBuilder = new StringBuilder();
            if(string.IsNullOrWhiteSpace(Index))
                pathBuilder.Append(Index);

            if (string.IsNullOrWhiteSpace(DocumentType))
            { 
                if(pathBuilder.Length > 0)
                    pathBuilder.Append("/");
                pathBuilder.Append(DocumentType);
            }
                
            if(pathBuilder.Length > 0)
                pathBuilder.Append("/");
            pathBuilder.Append(_MULTI_GET_VALUE);

            pathBuilder.Append(BuildQueryString());

            return new Uri(clusterUriProvider.ClusterUri, pathBuilder.ToString());
        }

        public override string BuildQueryString()
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Routing))
            {
                builder = HttpRequest.AddToQueryString(builder, _ROUTING_KEY, Routing);
            }

            if (builder.Length > 0)
                return null;

            return builder.ToString();
        }
    }
}

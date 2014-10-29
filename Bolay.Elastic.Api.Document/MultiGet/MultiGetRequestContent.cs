using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.MultiGet
{
    public class MultiGetRequestContent
    {
        private IEnumerable<MultiGetRequestedDocument> _Documents { get; set; }
        private IEnumerable<string> _DocumentIds { get; set; }

        /// <summary>
        /// Use this when you need to specify different indexes or types
        /// for the various documents you expect to collect.
        /// Do not populate both Documents and DocumentIds.
        /// </summary>
        [JsonProperty("docs")]
        public IEnumerable<MultiGetRequestedDocument> Documents 
        {
            get { return _Documents; }
            set
            {
                if (value != null && value.Any())
                {
                    if (_DocumentIds != null && _DocumentIds.Any())
                        throw new ArgumentException("Documents", "Documents and DocumentIds should not both be populated.");
                    _Documents = value;
                }
                else
                {
                    _Documents = null;
                }                
            }
        }

        /// <summary>
        /// Use this if all the documents are in the same index and type.
        /// Making sure to provide the index and type in the MultiGetDocumentRequest.
        /// Do not populate both Documents and DocumentIds.
        /// </summary>
        [JsonProperty("ids")]
        public IEnumerable<string> DocumentIds
        {
            get { return _DocumentIds; }
            set
            {
                if (value != null && value.Any())
                {
                    if (_Documents != null && _Documents.Any())
                        throw new ArgumentException("DocumentIds", "DocumentIds and Documents should not both be populated.");
                    _DocumentIds = value;
                }
                else
                {
                    _DocumentIds = null;
                }                
            }
        }

        [JsonIgnore]
        public bool IsNull
        { 
            get
            {
                if ((Documents == null || !Documents.Any()) && (DocumentIds == null || !DocumentIds.Any()))
                    return true;

                return false;
            }
        }

        [JsonIgnore]
        public bool AllIndexesSpecifiedInDocuments
        {
            get 
            {
                if (Documents == null || !Documents.Any())
                    return false;

                if(Documents.All(x => !string.IsNullOrWhiteSpace(x.Index)))
                    return true;

                return false;
            }
        }

        [JsonIgnore]
        public bool NoIndexesSpecifiedInDocuments
        { 
            get 
            {
                if (Documents == null || !Documents.Any())
                    return true;

                if(Documents.All(x => string.IsNullOrWhiteSpace(x.Index)))
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Create a multi get content item via documents. Do not fill document ids after using this constructor.
        /// </summary>
        /// <param name="documents"></param>
        public MultiGetRequestContent(IEnumerable<MultiGetRequestedDocument> documents)
        {
            if (documents == null || !documents.Any())
                throw new ArgumentNullException("documents", "MutiGetContent requires either documents or document ids.");

            Documents = documents;
        }

        public MultiGetRequestContent(IEnumerable<string> documentIds)
        {
            if (documentIds == null || !documentIds.Any())
                throw new ArgumentNullException("documentIds", "MultiGetContent requires documentIds or documents.");

            DocumentIds = documentIds;
        }
    }
}

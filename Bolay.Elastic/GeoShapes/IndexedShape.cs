using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    [JsonConverter(typeof(IndexedShapeSerializer))]
    public class IndexedShape
    {
        public string Index { get; private set; }
        public string DocumentType { get; private set; }
        public string DocumentId { get; private set; }
        public string PropertyPath { get; private set; }

        /// <summary>
        /// Create an indexed shape object.
        /// </summary>
        /// <param name="index">The index to find the geo_shape exists in.</param>
        /// <param name="documentType">The document type the geo_shape exists in.</param>
        /// <param name="documentId">The identifier of the document the geo_shape exists in.</param>
        /// <param name="propertyPath">The path to the property the geo_shape exists in.</param>
        public IndexedShape(string index, string documentType, string documentId, string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "Indexed shape requires an index.");
            if (string.IsNullOrWhiteSpace(documentType))
                throw new ArgumentNullException("documentType", "Indexed shape requires a document type.");
            if (string.IsNullOrWhiteSpace(documentId))
                throw new ArgumentNullException("documentId", "Indexed shape requires a document id.");
            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentNullException("propertyPath", "Indexed shape requires a property path.");

            Index = index;
            DocumentType = documentType;
            DocumentId = documentId;
            PropertyPath = propertyPath;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    [JsonConverter(typeof(DocumentPropertySerializer))]
    public interface IDocumentProperty
    {       
        /// <summary>
        /// Gets the name of the document property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        PropertyTypeEnum PropertyType { get; }
    }

    [JsonConverter(typeof(DocumentPropertyCollectionSerializer))]
    internal class DocumentPropertyCollection : List<IDocumentProperty> 
    {
        public DocumentPropertyCollection() { }

        public DocumentPropertyCollection(IEnumerable<IDocumentProperty> properties)
        {
            if (properties == null || properties.All(x => x == null))
                throw new ArgumentNullException("properties", "DocumentPropertyCollection requires at least one property.");

            this.AddRange(properties.Where(x => x != null));
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.Attachment
{
    [JsonConverter(typeof(AttachmentPropertySerializer))]
    public class AttachmentProperty : DocumentPropertyBase
    {
        /// <summary>
        /// Gets or sets the fields of the attachment.
        /// </summary>
        public IEnumerable<IDocumentProperty> Fields { get; set; }

        public AttachmentProperty(string name) : base(name, PropertyTypeEnum.Attachment) { }
    }
}

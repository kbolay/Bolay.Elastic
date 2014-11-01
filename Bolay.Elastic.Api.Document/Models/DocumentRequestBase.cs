using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public abstract class DocumentRequestBase
    {
        /// <summary>
        /// The index to operate in.
        /// </summary>
        public string Index { get; protected set; }

        /// <summary>
        /// Specify the document type.
        /// </summary>
        public string DocumentType { get; protected set; }

        /// <summary>
        /// The _id of the document.
        /// </summary>
        public string DocumentId { get; protected set; }
    }
}

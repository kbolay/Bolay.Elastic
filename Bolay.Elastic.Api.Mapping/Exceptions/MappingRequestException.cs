using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Exceptions
{
    public class MappingRequestException : Exception
    {
        public Uri MappingUri { get; set; }

        public MappingRequestException(Uri mappingUri) : this(mappingUri, null, null) { }
        public MappingRequestException(Uri mappingUri, string message) : this(mappingUri, message, null) { }
        public MappingRequestException(Uri mappingUri, string message, Exception innerException) : base(message, innerException)
        {
            this.MappingUri = mappingUri;
        }
    }
}

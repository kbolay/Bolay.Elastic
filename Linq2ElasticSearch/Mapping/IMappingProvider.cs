using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch.Mapping
{
    public interface IMappingProvider
    {
        ModelMapping<T> GetDocumentMapping<T>(DocumentType<T> documentType);
    }
}

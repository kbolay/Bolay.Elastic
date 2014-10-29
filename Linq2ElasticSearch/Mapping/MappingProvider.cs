using Bolay.Elastic.Api.Mapping;
using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Mapping.Types.RootObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch.Mapping
{
    public class MappingProvider : IMappingProvider
    {
        private readonly IndexMapping _DefaultMapping;
        private IMappingRepository _MappingRepository { get; set; }

        private IEnumerable<IndexMapping> _RealMapping { get; set; }

        public MappingProvider(IMappingRepository mappingRepository, IndexMapping defaultMapping) 
        {
            _DefaultMapping = defaultMapping;
            _MappingRepository = mappingRepository;
            Task.Factory.StartNew(() =>
            {
                _RealMapping = mappingRepository.GetClusterMapping();
            });
        }

        public ModelMapping<T> GetDocumentMapping<T>(DocumentType<T> documentType) where T : class
        {
 	        throw new NotImplementedException();
        }
    }
}

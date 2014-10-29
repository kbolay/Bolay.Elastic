using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Alias
{
    public class AliasRepository : IAliasRepository
    {
        private readonly Uri _ClusterUri;
        private readonly bool _AfterVersion_0_90_1;

        public AliasRepository(IUriProvider clusterUri, bool afterVersion_0_90_1 = true)
        {
            if (clusterUri == null || clusterUri.Uri == null)
                throw new ArgumentNullException("clusterUri", "Cluster Uri is required.");

            _ClusterUri = clusterUri.Uri;
            _AfterVersion_0_90_1 = afterVersion_0_90_1;
        }

        public Models.AliasResponse Get(string index = "*", string alias = "*")
        {
            throw new NotImplementedException();
        }

        public bool Add(string index, string alias, string filter = null, string search_routing = null, string index_routing = null)
        {
            throw new NotImplementedException();
        }

        public bool Add(List<string> indexes, string alias, string filter = null, string search_routing = null, string index_routing = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(string index, string alias, string filter = null, string search_routing = null, string index_routing = null)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string index, string alias)
        {
            throw new NotImplementedException();
        }
    }
}

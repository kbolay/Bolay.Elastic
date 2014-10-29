using Bolay.Elastic.Api.Health.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health
{
    public interface IHealthRepository
    {
        Cluster Get(HealthRequest healthRequest = null);
        IEnumerable<Index> GetAlias(string alias, HealthRequest healthRequest = null);
        IEnumerable<Index> GetIndices(IEnumerable<string> indexNames, HealthRequest healthRequest = null);
        Index GetIndex(string indexName, HealthRequest healthRequest = null);
        Shard GetShard(string indexName, string shard, HealthRequest healthRequest = null);
    }
}

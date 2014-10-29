using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Interfaces
{
    public interface IElasticUriProvider : IUriProvider
    {
        Uri ClusterUri { get; }
    }
}

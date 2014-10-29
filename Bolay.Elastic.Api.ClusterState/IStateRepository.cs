using Bolay.Elastic.Api.ClusterState.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState
{
    public interface IStateRepository
    {
        State Get(StateRequest request = null);
    }
}

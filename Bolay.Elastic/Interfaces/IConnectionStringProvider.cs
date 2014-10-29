using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Interfaces
{
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; }
    }
}

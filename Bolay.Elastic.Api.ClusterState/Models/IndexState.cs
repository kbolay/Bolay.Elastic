using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class IndexState : TypeSafeEnumBase<IndexState>
    {
        public static readonly IndexState Open = new IndexState("open");
        public static readonly IndexState Closed = new IndexState("closed");

        private IndexState(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}

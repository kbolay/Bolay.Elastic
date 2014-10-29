using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public sealed class WriteConsistencyEnum : TypeSafeEnumBase<WriteConsistencyEnum>
    {
        public static readonly WriteConsistencyEnum OneShard = new WriteConsistencyEnum("one");
        public static readonly WriteConsistencyEnum QuorumOfShards = new WriteConsistencyEnum("quorom");
        public static readonly WriteConsistencyEnum AllShards = new WriteConsistencyEnum("all");

        private WriteConsistencyEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}

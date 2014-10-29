using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public sealed class LinkType : TypeSafeEnumBase<LinkType>
    {
        public static readonly LinkType And = new LinkType("AND");
        public static readonly LinkType Or = new LinkType("OR");
        public static readonly LinkType Not = new LinkType("NOT");

        private LinkType(string value) 
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}

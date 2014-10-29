using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public sealed class RequestSectionType : TypeSafeEnumBase<RequestSectionType>
    {
        public static readonly RequestSectionType Facets = new RequestSectionType("facets");
        public static readonly RequestSectionType Sort = new RequestSectionType("sort");
        public static readonly RequestSectionType Query = new RequestSectionType("query");
        public static readonly RequestSectionType Filters = new RequestSectionType("fitlers");
        public static readonly RequestSectionType Select = new RequestSectionType("select");

        private RequestSectionType(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}

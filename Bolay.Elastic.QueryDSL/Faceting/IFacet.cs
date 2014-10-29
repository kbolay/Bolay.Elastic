using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Faceting
{
    [JsonConverter(typeof(FacetSerializer))]
    public interface IFacet
    {
        string FacetName { get; }
        //int Size { get; set; }
        //string NestedObject { get; set; }
        //IFilter FacetFilter { get; set; }
        //bool IsScopeGlobal { get; set; }
    }
}

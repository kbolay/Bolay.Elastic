using Bolay.Elastic.QueryDSL.Suggesters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Search.RequestBody
{
    public class SuggestSearchDocument
    {
        public Suggest Suggest { get; set; }
    }
}

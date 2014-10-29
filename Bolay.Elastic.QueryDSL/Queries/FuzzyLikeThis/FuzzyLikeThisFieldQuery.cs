using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis
{
    public class FuzzyLikeThisFieldQuery : FuzzyLikeThisBase
    {
        internal FuzzyLikeThisFieldQuery() : base() { }

        public FuzzyLikeThisFieldQuery(string field, string query)
            : base(new List<string>() { field }, query)
        { }
    }
}

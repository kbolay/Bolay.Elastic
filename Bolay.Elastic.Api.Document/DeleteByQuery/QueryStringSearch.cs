using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.DeleteByQuery
{
    public class QueryStringSearch
    {
        public const string _QUERY_KEY = "q";
        public const string _DEFAULT_FIELD_KEY = "df";
        public const string _ANALYZER_KEY = "analyzer";
        public const string _DEFAULT_OPERATOR_KEY = "default_operator";

        public const string _AND_OPERATOR_VALUE = "AND";

        /// <summary>
        /// The query used to select documents.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Default field for the query.
        /// </summary>
        public string DefaultField { get; set; }

        /// <summary>
        /// Analyzer for the query.
        /// </summary>
        public string Analyzer { get; set; }

        /// <summary>
        /// Set the default operator for the query.
        /// </summary>
        public bool UseAndForDefaultOperator { get; set; }
    }
}

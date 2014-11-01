using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.DeleteByQuery
{
    public class QueryStringSearch
    {
        internal const string QUERY_KEY = "q";
        internal const string DEFAULT_FIELD_KEY = "df";
        internal const string ANALYZER_KEY = "analyzer";
        internal const string DEFAULT_OPERATOR_KEY = "default_operator";

        internal const string AND_OPERATOR_VALUE = "AND";

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

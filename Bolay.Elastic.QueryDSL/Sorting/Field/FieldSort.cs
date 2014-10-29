using Bolay.Elastic.QueryDSL.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Field
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/search-request-sort.html
    /// </summary>
    [JsonConverter(typeof(FieldSerializer))]
    public class FieldSort : ISortClause
    {
        /// <summary>
        /// Gets the field to sort on.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets or sets the reverse value.
        /// </summary>
        public bool Reverse { get; set; }

        /// <summary>
        /// Gets or sets the order to sort in.
        /// Defaults to ascending.
        /// </summary>
        public SortOrderEnum SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the method used to sort arrays.
        /// </summary>
        public SortModeEnum SortMode { get; set; }

        /// <summary>
        /// Gets or sets whether the search will fail based on an attempt to sort a non-mapped field.
        /// Defaults to false, which will cause the search to fail if there is an attempt to sort on a non-mapped field.
        /// </summary>
        public bool IgnoreUnmappedField { get; set; }

        /// <summary>
        /// Gets the filter to be applied before sorting.
        /// </summary>
        public IFilter NestedFilter { get; private set; }

        /// <summary>
        /// Gets the nested_object to sort on. The sort field must be a value directly in this object.
        /// </summary>
        public string NestedPath { get; set; }

        /// <summary>
        /// Gets or sets the missing value for the sort clause.
        /// </summary>
        public MissingValue Missing { get; set; }

        /// <summary>
        /// Create a sort clause by a field.
        /// </summary>
        /// <param name="field">Sets the field to sort on.</param>
        public FieldSort(string field)
        {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException("field", "FieldSort needs a field to sort on.");

            Field = field;
            SortOrder = SortClauseSerializer._ORDER_DEFAULT;
        }

        /// <summary>
        /// Create a sort clause using a nested fitler.
        /// </summary>
        /// <param name="field">Sets the field to sort on.</param>
        /// <param name="nestedFilter">Sets the nested_filter to exclude some documents from sorting.</param>
        public FieldSort(string field, IFilter nestedFilter) 
            : this(field)
        {
            if (nestedFilter == null)
                throw new ArgumentNullException("nestedFilter", "FieldSort requires a nested filter for this constructor.");

            NestedFilter = nestedFilter;
        }

        /// <summary>
        /// Creates a sort clause using a nested path.
        /// </summary>
        /// <param name="field">Sets the field to sort on.</param>
        /// <param name="nestedPath">Sets the nested_object to sort on. The sort field must be a value directly in this object.</param>
        public FieldSort(string field, string nestedPath)
            : this(field)
        {
            if (string.IsNullOrWhiteSpace(nestedPath))
                throw new ArgumentNullException("nestedPath", "FieldSort requires a nested path for this constructor.");

            NestedPath = nestedPath;
        }
    }
}

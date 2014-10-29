using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting
{
    public interface ISortClause : ISearchPiece
    {
        /// <summary>
        /// Gets the field to sort on.
        /// </summary>
        //string Field { get; }

        // TODO: Find if sort order is actually required to be serialized in the query.
        // TODO: Confirm that default really is ascending.

        /// <summary>
        /// Gets the order to sort the values into. Ascending or descending.
        /// </summary>
        SortOrderEnum SortOrder { get; }

        /// <summary>
        /// Gets or sets the reverse value.
        /// </summary>
        bool Reverse { get; }
    }
}

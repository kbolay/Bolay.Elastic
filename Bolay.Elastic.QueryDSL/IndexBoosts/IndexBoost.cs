using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.IndexBoosts
{
    public class IndexBoost
    {
        /// <summary>
        /// Gets the index from which documents should receive boosted scores.
        /// </summary>
        public string Index { get; private set; }

        /// <summary>
        /// Gets the boost value to apply to documents from this index.
        /// </summary>
        public Double Boost { get; private set; }

        public IndexBoost(string index, Double boost)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "IndexBoost requires an index.");

            Index = index;
            Boost = boost;
        }
    }
}

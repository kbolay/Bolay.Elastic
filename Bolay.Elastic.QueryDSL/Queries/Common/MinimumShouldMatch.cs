using Bolay.Elastic.QueryDSL.MinimumShouldMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.Common
{
    public class MinimumShouldMatch
    {
        public MinimumShouldMatchBase All { get; private set; }
        public MinimumShouldMatchBase LowFrequency { get; private set; }
        public MinimumShouldMatchBase HighFrequency { get; private set; }

        internal MinimumShouldMatch() { }

        public MinimumShouldMatch(MinimumShouldMatchBase all)
        {
            if (all == null)
                throw new ArgumentNullException("all", "MinimumShouldMatch all value must be populated in this constructor.");

            All = all;
        }

        public MinimumShouldMatch(MinimumShouldMatchBase lowFrequency, MinimumShouldMatchBase highFrequency)
        {
            if (lowFrequency == null && highFrequency == null)
                throw new ArgumentNullException("frequency", "MinimumShouldMatch high or low frequency value must be populated in this constructor.");

            LowFrequency = lowFrequency;
            HighFrequency = highFrequency;
        }
    }
}

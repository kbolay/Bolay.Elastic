using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties
{
    public class Frequency
    {
        private const string _MINIMUM = "min";
        private const string _MAXIMUM = "max";
        private const string _MINIMUM_SEGMENT_SIZE = "min_segment_size";

        internal const Double _MINIMUM_DEFAULT = default(Double);
        internal const Double _MAXIMUM_DEFAULT = default(Double);
        internal const Int64 _MINIMUM_SEGMENT_SIZE_DEFAULT = default(Int64);

        /// <summary>
        /// Gets the minimum frequency a term must appear.
        /// </summary>
        public Double Minimum { get; private set; }

        /// <summary>
        /// Gets the maximum frequency a term must appear.
        /// </summary>
        public Double Maximum { get; private set; }

        /// <summary>
        /// Gets the minimum segment size.
        /// </summary>
        public Int64 MinimumSegmentSize { get; private set; }

        /// <summary>
        /// Create the required frequency.
        /// </summary>
        /// <param name="minimum">Sets the minimum frequency a term must appear to be affected.</param>
        /// <param name="maximum">Sets the maximum frequency a term can appear to be affected.</param>
        public Frequency(Double minimum, Double maximum)
        {
            if (minimum <= 0)
                throw new ArgumentOutOfRangeException("minimum", "Frequency minimum value must be greater than zero.");
            if (maximum <= 0 || maximum < minimum)
                throw new ArgumentOutOfRangeException("maximum", "Frequency maximum value must be greater than minimum.");

            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Create the required frequency with a minimum segment size.
        /// </summary>
        /// <param name="minimum">Sets the minimum frequency a term must appear to be affected.</param>
        /// <param name="maximum">Sets the maximum frequency a term can appear to be affected.</param>
        /// <param name="minimumSegmentSize">Sets the maximum segment size.</param>
        public Frequency(Double minimum, Double maximum, Int64 minimumSegmentSize)
            : this(minimum, maximum)
        {
            if (minimumSegmentSize <= 0)
                throw new ArgumentOutOfRangeException("minimumSegmentSize", "Frequency minimum segment size must be greater than zero.");

            MinimumSegmentSize = minimumSegmentSize;
        }

        internal Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(_MINIMUM, this.Minimum, _MINIMUM_DEFAULT);
            fieldDict.AddObject(_MAXIMUM, this.Maximum, _MAXIMUM_DEFAULT);
            fieldDict.AddObject(_MINIMUM_SEGMENT_SIZE, this.MinimumSegmentSize, _MINIMUM_SEGMENT_SIZE_DEFAULT);

            return fieldDict;
        }

        internal static Frequency Deserialize(Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return null;

            if (fieldDict.ContainsKey(_MINIMUM_SEGMENT_SIZE))
            {
                return new Frequency(fieldDict.GetDouble(_MINIMUM), fieldDict.GetDouble(_MAXIMUM), fieldDict.GetInt64(_MINIMUM_SEGMENT_SIZE));
            }

            return new Frequency(fieldDict.GetDouble(_MINIMUM), fieldDict.GetDouble(_MAXIMUM));
        }
    }
}

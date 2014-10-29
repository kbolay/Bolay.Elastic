using Bolay.Elastic.QueryDSL.Aggregations.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Aggregations
{
    public abstract class BucketAggregationBase : IAggregation
    {
        internal const string _SUB_AGGREGATIONS = "aggregations";
        internal const string _SUB_AGGREGATIONS_ABBR = "aggs";

        private IEnumerable<IAggregation> _SubAggregations { get; set; }

        /// <summary>
        /// Gets the aggregation name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the sub aggregations.
        /// </summary>
        public IEnumerable<IAggregation> SubAggregations 
        {
            get { return _SubAggregations; }
            set 
            {
                if (value == null || value.All(x => x == null))
                {
                    _SubAggregations = null;
                    return;
                }                    

                if (value.Any(x => x is GlobalAggregate))
                    throw new Exception("GlobalAggregate cannot be a sub aggregate.");

                _SubAggregations = value.Where(x => x != null);
            }
        }

        /// <summary>
        /// Creates a bucket aggregation.
        /// </summary>
        /// <param name="name">Sets the aggregation name.</param>
        public BucketAggregationBase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All bucket aggregations require a name.");

            Name = name;
        }

        internal Dictionary<string, object> SerializeSubAggregations()
        {
            if (SubAggregations == null)
                return null;

            Dictionary<string, object> subAggsDict = new Dictionary<string, object>();
            foreach (IAggregation aggregate in SubAggregations)
            {
                if (aggregate == null)
                    continue;

                string aggJson = JsonConvert.SerializeObject(aggregate);
                Dictionary<string, object> aggDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggJson);
                subAggsDict.Add(aggDict.First().Key, aggDict.First().Value);
            }

            return subAggsDict;
        }

        internal static IEnumerable<IAggregation> DeserializeSubAggregations(Dictionary<string, object> aggDict)
        {
            Dictionary<string, object> subAggsDict = null;
            if (aggDict.ContainsKey(BucketAggregationBase._SUB_AGGREGATIONS))
            {
                subAggsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(_SUB_AGGREGATIONS));
            }
            else if (aggDict.ContainsKey(BucketAggregationBase._SUB_AGGREGATIONS_ABBR))
            {
                subAggsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggDict.GetString(_SUB_AGGREGATIONS_ABBR));
            }
            else
                return null;

            List<IAggregation> subAggregations = new List<IAggregation>();

            AggregationTypeEnum aggType = AggregationTypeEnum.Average;
            foreach (KeyValuePair<string, object> aggKvp in subAggsDict)
            {
                Dictionary<string, object> aggTypeDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(aggKvp.Value.ToString());

                aggType = AggregationTypeEnum.Find(aggTypeDict.First().Key);
                if(aggType == null)
                    throw new Exception(aggTypeDict.First().Key + " is not a value aggregation type.");

                Dictionary<string, object> subAggDict = new Dictionary<string, object>();
                subAggDict.Add(aggKvp.Key, aggKvp.Value);

                string subAggJson = JsonConvert.SerializeObject(subAggDict);
                subAggregations.Add(JsonConvert.DeserializeObject(subAggJson, aggType.ImplementationType) as IAggregation);
            }

            return subAggregations;
        }
    }
}

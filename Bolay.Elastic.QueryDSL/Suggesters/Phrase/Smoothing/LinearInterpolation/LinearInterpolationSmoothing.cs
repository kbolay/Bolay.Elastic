using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.LinearInterpolation
{
    [JsonConverter(typeof(LinearInterpolationSerializer))]
    public class LinearInterpolationSmoothing : ISmoothing
    {
        public Double UnigramWeight { get; private set; }
        public Double BigramWeight { get; private set; }
        public Double TrigramWeight { get; private set; }
        
        public LinearInterpolationSmoothing(Double unigramWeight, Double bigramWeight, Double trigramWeight)
        {
            UnigramWeight = unigramWeight;
            BigramWeight = bigramWeight;
            TrigramWeight = trigramWeight;
        }
    }
}

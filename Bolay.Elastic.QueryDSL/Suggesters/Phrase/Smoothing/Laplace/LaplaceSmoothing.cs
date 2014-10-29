using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.Laplace
{
    [JsonConverter(typeof(LaplaceSerializer))]
    public class LaplaceSmoothing : ISmoothing
    {
        /// <summary>
        /// Gets the additive value.
        /// </summary>
        public Double Alpha { get; set; }

        public LaplaceSmoothing()
        {
            Alpha = LaplaceSerializer._ALPHA_DEFAULT;
        }
    }
}

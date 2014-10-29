using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Suggesters.Phrase.Smoothing.StupidBackoff
{
    [JsonConverter(typeof(StupidSerializer))]
    public class StupidBackoffSmoothing : ISmoothing
    {
        public Double Discount { get; set; }

        public StupidBackoffSmoothing()
        {
            Discount = StupidSerializer._DISCOUNT_DEFAULT;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.HtmlStrip
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-htmlstrip-charfilter.html
    /// </summary>
    [JsonConverter(typeof(HtmlStripCharacterFilterSerializer))]
    public class HtmlStripCharacterFilter : CharacterFilterBase
    {
        /// <summary>
        /// Create an html strip character filter.
        /// </summary>
        /// <param name="name">Sets the name of the character filter.</param>
        public HtmlStripCharacterFilter(string name) : base(name, CharacterFilterTypeEnum.HtmlStrip) { }
    }
}

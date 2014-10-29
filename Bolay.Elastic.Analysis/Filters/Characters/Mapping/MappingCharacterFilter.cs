using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.Mapping
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-mapping-charfilter.html
    /// </summary>
    [JsonConverter(typeof(MappingCharacterFilterSerializer))]
    public class MappingCharacterFilter : CharacterFilterBase
    {
        /// <summary>
        /// Gets the character mappings.
        /// </summary>
        public IEnumerable<CharacterMapping> Mappings { get; set; }

        /// <summary>
        /// Gets the path to the mappings configuration file.
        /// </summary>
        public string MappingsPath { get; set; }

        /// <summary>
        /// Creates the mapping character filter.
        /// </summary>
        /// <param name="name">Sets the name of the character filter.</param>
        /// <param name="mappings">Sets the mappings of the character filter.</param>
        public MappingCharacterFilter(string name, IEnumerable<CharacterMapping> mappings) 
            : base(name, CharacterFilterTypeEnum.Mapping) 
        {
            if (mappings == null || mappings.All(x => x == null))
            {
                throw new ArgumentNullException("mappings", "MappingCharacterFilter requires at least one character mapping in this constructor.");
            }

            Mappings = mappings.Where(x => x != null);
        }

        /// <summary>
        /// Creates a mapping character filter.
        /// </summary>
        /// <param name="name">Sets the name of the character filter.</param>
        /// <param name="mappingsPath">Sets the path of the mapping configuration file.</param>
        public MappingCharacterFilter(string name, string mappingsPath)
            : base(name, CharacterFilterTypeEnum.Mapping)
        {
            if (string.IsNullOrWhiteSpace(mappingsPath))
            {
                throw new ArgumentNullException("mappingsPath", "MappingCharacterFilter requires a path in this constructor.");
            }

            MappingsPath = mappingsPath;
        }
    }
}

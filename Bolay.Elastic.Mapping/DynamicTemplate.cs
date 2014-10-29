using Bolay.Elastic.Api.Serialization;
using Bolay.Elastic.Mapping.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    [JsonConverter(typeof(DynamicTemplateSerializer))]
    public class DynamicTemplate
    {
        internal const string _IS_MATCH_PATTERN_REGEX_TRUE_VALUE = "regex";
        internal const bool _IS_MATCH_PATTERN_REGEX = false;

        /// <summary>
        /// Gets the name of the dynamic template.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the property pattern to match on.
        /// </summary>
        public string Match { get; set; }

        /// <summary>
        /// Gets or sets the property pattern not to match on.
        /// </summary>
        public string UnMatch { get; set; }

        /// <summary>
        /// Gets or sets the path pattern to match on.
        /// </summary>
        public string PathMatch { get; set; }

        /// <summary>
        /// Gets or sets the path pattern not to match on.
        /// </summary>
        public string PathUnMatch { get; set; }

        /// <summary>
        /// Gets or sets whether the match patterns given will be treated as regular expressions.
        /// Defaults to false.
        /// </summary>
        public bool IsMatchPatternRegex { get; set; }

        /// <summary>
        /// Gets or sets the type of property this mapping template will apply to.
        /// </summary>
        public PropertyTypeEnum MatchMappingType { get; set; }

        /// <summary>
        /// Gets the mapping that will be applied to matching patterns.
        /// </summary>
        public IDocumentProperty Mapping { get; private set; }

        /// <summary>
        /// Gets a mapping provided in json format. 
        /// </summary>
        public string MappingJson { get; private set; }

        /// <summary>
        /// Gets or sets whether {name} is used in the dynamic template.
        /// </summary>
        public bool UseDynamicName { get; set; }

        /// <summary>
        /// Gets or sets whether the {dynamic_type} is used at the top level.
        /// </summary>
        //public bool UseDynamicType { get; set; }

        private DynamicTemplate(string name, string pattern = "*", bool isMatchingPattern = true, bool isPathPattern = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "DynamicTemplate requires a name.");

            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern", "DynamicTemplate requires a pattern value.");

            Name = name;

            if (isMatchingPattern)
            {
                if (!isPathPattern)
                    Match = pattern;
                else
                    PathMatch = pattern;
            }
            else
            {
                if (!isPathPattern)
                    UnMatch = pattern;
                else
                    PathUnMatch = pattern;
            }
            
        }

        /// <summary>
        /// Create a dynamic_template for mapping.
        /// </summary>
        /// <param name="name">Sets the name of the dynamic_template.</param>
        /// <param name="mapping">Sets the mapping for this dynamic_template.</param>
        /// <param name="pattern">Sets the pattern.</param>
        /// <param name="isMatchingPattern">Sets if the pattern is for matching or "un" matching.</param>
        /// <param name="isPathPattern">Sets if the pattern is a path pattern.</param>
        public DynamicTemplate(string name, IDocumentProperty mapping, string pattern = "*", bool isMatchingPattern = true, bool isPathPattern = false)
            : this(name, pattern, isMatchingPattern, isPathPattern)
        {           
            if (mapping == null)
                throw new ArgumentNullException("mapping", "DynamicTemplate requires a mapping value.");
            Mapping = mapping; 
        }

        /// <summary>
        /// Create a dynamic_template for mapping. Use this constructor for use of the {dynamic_type} value or extended use of the {name} value.
        /// </summary>
        /// <param name="name">Sets the name of the dynamic_template.</param>
        /// <param name="mappingJson">Sets the mapping for this dynamic_template.</param>
        /// <param name="pattern">Sets the pattern.</param>
        /// <param name="isMatchingPattern">Sets if the pattern is for matching or "un" matching.</param>
        /// <param name="isPathPattern">Sets if the pattern is a path pattern.</param>
        public DynamicTemplate(string name, string mappingJson, string pattern = "*", bool isMatchingPattern = true, bool isPathPattern = false)
            : this(name, pattern, isMatchingPattern, isPathPattern)
        {
            if (string.IsNullOrWhiteSpace(mappingJson))
                throw new ArgumentNullException("mappingJson", "DynamicTemplate requires a json mapping value in this constructor.");

            MappingJson = mappingJson;
        } 
    }
}

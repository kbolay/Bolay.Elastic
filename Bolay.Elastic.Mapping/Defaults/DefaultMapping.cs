using Bolay.Elastic.Mapping.Types;
using Bolay.Elastic.Mapping.Types.Object;
using Bolay.Elastic.Mapping.Types.RootObject;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Defaults
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-dynamic-mapping.html
    /// </summary>
    [JsonConverter(typeof(DefaultMappingSerializer))]
    public class DefaultMapping
    {
        internal const bool _IS_ENABLED_DEFAULT = true;
        internal static DynamicSettingEnum _DYNAMIC_DEFAULT = DynamicSettingEnum.True;
        internal const bool _INCLUDE_IN_ALL_DEFAULT = true;
        internal const bool _DETECT_DATES_DEFAULT = true;
        internal const bool _DETECT_NUMBERS_DEFAULT = false;

        /// <summary>
        /// Gets the name of the root object, this is a document type for the index.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the default analyzer information for the entire object.
        /// </summary>
        public PropertyAnalyzer Analyzer { get; set; }

        /// <summary>
        /// Gets or sets whether to attempt to detect date fields.
        /// Defaults to true.
        /// </summary>
        public bool DetectDates { get; set; }

        /// <summary>
        /// Gets or sets whether to attempt to detect numeric fields.
        /// Defaults to false.
        /// </summary>
        public bool DetectNumbers { get; set; }

        public IEnumerable<DynamicTemplate> DynamicTemplates { get; set; }

        /// <summary>
        /// Gets or sets the dynamic value of the object.
        /// Defaults to true.
        /// </summary>
        public DynamicSettingEnum Dynamic { get; set; }

        /// <summary>
        /// Gets or sets the whether the object is indexed.
        /// Defaults to true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the copy_to value(s).
        /// </summary>        
        public IEnumerable<string> CopyTo { get; set; }

        /// <summary>
        /// Gets or sets whether this objects properties will be included in the all field by default. When set, it propagates down to all the inner mapping defined within the object that do no explicitly set it.
        /// Defaults to true.
        /// </summary>
        public bool IncludeInAll { get; set; }

        /// <summary>
        /// Gets or sets the formats for added date properties.
        /// </summary>
        public IEnumerable<DateFormat> DateFormats { get; set; }

        /// <summary>
        /// Create a root object property.
        /// </summary>
        /// <param name="name">Sets the name of the root object.</param>
        public DefaultMapping()
        {
            Dynamic = _DYNAMIC_DEFAULT;
            IsEnabled = _IS_ENABLED_DEFAULT;
            IncludeInAll = _INCLUDE_IN_ALL_DEFAULT;
            DetectDates = _DETECT_DATES_DEFAULT;
            DetectNumbers = _DETECT_NUMBERS_DEFAULT;
        }

        internal DefaultMapping(ObjectProperty prop)
        {
            this.CopyTo = prop.CopyTo;
            this.Dynamic = prop.Dynamic;
            this.IncludeInAll = prop.IncludeInAll;
            this.IsEnabled = prop.IsEnabled;
        }
    }
}

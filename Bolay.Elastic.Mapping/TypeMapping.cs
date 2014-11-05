using Bolay.Elastic.Mapping.Fields;
using Bolay.Elastic.Mapping.Fields._All;
using Bolay.Elastic.Mapping.Fields._Analyzer;
using Bolay.Elastic.Mapping.Fields._Id;
using Bolay.Elastic.Mapping.Fields._Index;
using Bolay.Elastic.Mapping.Fields._Parent;
using Bolay.Elastic.Mapping.Fields._Routing;
using Bolay.Elastic.Mapping.Fields._Size;
using Bolay.Elastic.Mapping.Fields._Source;
using Bolay.Elastic.Mapping.Fields._Timestamp;
using Bolay.Elastic.Mapping.Fields._Ttl;
using Bolay.Elastic.Mapping.Fields._Type;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Mapping.Properties.Object;
using Bolay.Elastic.Time;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    [JsonConverter(typeof(TypeMappingSerializer))]
    public class TypeMapping
    {
        internal const string DYNAMIC = "dynamic";
        internal const string IS_ENABLED = "enabled";
        internal const string INCLUDE_IN_ALL = "include_in_all";
        internal const string PROPERTIES = "properties";
        internal const string DYNAMIC_DATE_FORMATS = "dynamic_date_formats";
        internal const string DETECT_DATES = "date_detection";
        internal const string DETECT_NUMBERS = "numeric_detection";
        internal const string DYNAMIC_TEMPLATES = "dynamic_templates";
        internal const string META = "_meta";

        internal const bool _INCLUDE_IN_ALL_DEFAULT = true;
        internal const bool _IS_ENABLED_DEFAULT = true;
        internal static DynamicSettingEnum _DYNAMIC_DEFAULT = DynamicSettingEnum.True;
        internal static PropertyTypeEnum _PROPERTY_TYPE_DEFAULT = PropertyTypeEnum.Object;
        internal const bool _DETECT_DATES_DEFAULT = true;
        internal const bool _DETECT_NUMBERS_DEFAULT = false;

        /// <summary>
        /// Gets the name of the root object, this is a document type for the index.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the _* fields for this document type.
        /// </summary>
        public DocumentMapping Fields { get; set; }

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

        /// <summary>
        /// Gets or sets the dynamic templates of the root object.
        /// </summary>
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
        /// Gets or sets whether this objects properties will be included in the all field by default. When set, it propagates down to all the inner mapping defined within the object that do no explicitly set it.
        /// Defaults to true.
        /// </summary>
        public bool IncludeInAll { get; set; }

        /// <summary>
        /// Gets or sets properties of this object.
        /// </summary>
        public IEnumerable<IDocumentProperty> Properties { get; set; }

        /// <summary>
        /// Gets or sets the formats for dynamically added date properties.
        /// </summary>
        public IEnumerable<DateFormat> DynamicDateFormats { get; set; }

        /// <summary>
        /// Gets or sets the _meta information of the mapping type.
        /// </summary>
        public Dictionary<string, object> MetaData { get; set; }

        /// <summary>
        /// Create a root object property.
        /// </summary>
        /// <param name="name">Sets the name of the root object.</param>
        public TypeMapping(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "RootObjectProperty requires a name.");

            Name = name;
            Dynamic = _DYNAMIC_DEFAULT;
            IsEnabled = _IS_ENABLED_DEFAULT;
            IncludeInAll = _INCLUDE_IN_ALL_DEFAULT;
            DetectDates = _DETECT_DATES_DEFAULT;
            DetectNumbers = _DETECT_NUMBERS_DEFAULT;
        }

        internal TypeMapping(ObjectProperty prop)
        {
            this.Name = prop.Name;
            this.Dynamic = prop.Dynamic;
            this.IncludeInAll = prop.IncludeInAll;
            this.IsEnabled = prop.IsEnabled;
            this.Properties = prop.Properties;
        }
    }
}

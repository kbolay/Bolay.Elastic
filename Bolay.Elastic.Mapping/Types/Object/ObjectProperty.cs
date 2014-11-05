using Bolay.Elastic.Api.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.Object
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/mapping-object-type.html
    /// </summary>
    [JsonConverter(typeof(ObjectPropertySerializer))]
    public class ObjectProperty : DocumentPropertyBase
    {
        private const string _DYNAMIC = "dynamic";
        private const string _IS_ENABLED = "enabled";
        private const string _COPY_TO = "copy_to";
        private const string _INCLUDE_IN_ALL = "include_in_all";
        private const string _PROPERTIES = "properties";
        private const string _TYPE = "type";

        internal const bool _IS_ENABLED_DEFAULT = true;
        internal static DynamicSettingEnum _DYNAMIC_DEFAULT = DynamicSettingEnum.True;
        internal static PropertyTypeEnum _PROPERTY_TYPE_DEFAULT = PropertyTypeEnum.Object;

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
        /// Gets or sets properties of this object.
        /// </summary>
        public IEnumerable<IDocumentProperty> Properties { get; set; }

        /// <summary>
        /// Create an object mapping property.
        /// </summary>
        /// <param name="name"></param>
        public ObjectProperty(string name)
            : base(name, _PROPERTY_TYPE_DEFAULT)
        {
            Dynamic = _DYNAMIC_DEFAULT;
            IsEnabled = _IS_ENABLED_DEFAULT;
            IncludeInAll = _INCLUDE_IN_ALL_DEFAULT;
        }

        internal ObjectProperty(string name, PropertyTypeEnum propertyType)
            : base(name, propertyType)
        {
            Dynamic = _DYNAMIC_DEFAULT;
            IsEnabled = _IS_ENABLED_DEFAULT;
            IncludeInAll = _INCLUDE_IN_ALL_DEFAULT;
        }

        internal static void Serialize(ObjectProperty prop, Dictionary<string, object> fieldDict)
        {
            if (prop == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_TYPE, prop.PropertyType.ToString(), _PROPERTY_TYPE_DEFAULT.ToString());
            fieldDict.AddObject(_INCLUDE_IN_ALL, prop.IncludeInAll, _INCLUDE_IN_ALL_DEFAULT);
            fieldDict.AddObject(_IS_ENABLED, prop.IsEnabled, _IS_ENABLED_DEFAULT);
            fieldDict.AddObject(_DYNAMIC, prop.Dynamic.RealValue, _DYNAMIC_DEFAULT.RealValue);

            if (prop.CopyTo != null && prop.CopyTo.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                int count = prop.CopyTo.Count(x => !string.IsNullOrWhiteSpace(x));

                if (count > 1)
                    fieldDict.AddObject(_COPY_TO, prop.CopyTo.Where(x => !string.IsNullOrWhiteSpace(x)));
                else
                    fieldDict.AddObject(_COPY_TO, prop.CopyTo.First(x => !string.IsNullOrWhiteSpace(x)));
            }

            if(prop.Properties != null && prop.Properties.Any(x => x != null))
                fieldDict.AddObject(_PROPERTIES, new DocumentPropertyCollection(prop.Properties));
        }

        internal static void Deserialize(ObjectProperty prop, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return;

            if (fieldDict.ContainsKey(_COPY_TO))
            {
                try
                {
                    prop.CopyTo = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_COPY_TO));
                }
                catch
                {
                    prop.CopyTo = new List<string>() { fieldDict.GetString(_COPY_TO) };
                }
            }

            prop.Dynamic = DynamicSettingEnum.Find(fieldDict.GetString(_DYNAMIC, _DYNAMIC_DEFAULT.ToString()));
            prop.IncludeInAll = fieldDict.GetBool(_INCLUDE_IN_ALL, _INCLUDE_IN_ALL_DEFAULT);
            prop.IsEnabled = fieldDict.GetBool(_IS_ENABLED, _IS_ENABLED_DEFAULT);

            if (fieldDict.ContainsKey(_PROPERTIES))
            {
                prop.Properties = JsonConvert.DeserializeObject<DocumentPropertyCollection>(fieldDict.GetString(_PROPERTIES));
            }
        }
    }
}

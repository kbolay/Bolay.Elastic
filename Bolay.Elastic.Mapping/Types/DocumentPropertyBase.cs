using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties
{
    public abstract class DocumentPropertyBase : MappingBase, IDocumentProperty
    {
        private const string _TYPE = "type";
        private const string _INDEX_NAME = "index_name";
        private const string _INCLUDE_IN_ALL = "include_in_all";

        internal static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.Analyzed;
        internal static PropertyTypeEnum _PROPERTY_TYPE_DEFAULT = PropertyTypeEnum.Object;
        internal const bool _STORE_DEFAULT = false;
        internal const bool _INCLUDE_IN_ALL_DEFAULT = true;

        /// <summary>
        /// Gets the name of the document property.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the index name of the property.
        /// Defaults to the name.
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// Gets the property type.
        /// </summary>
        public PropertyTypeEnum PropertyType { get; private set; }

        /// <summary>
        /// Gets or sets whether this objects properties will be included in the all field by default. When set, it propagates down to all the inner mapping defined within the object that do no explicitly set it.
        /// Defaults to true.
        /// </summary>
        public bool IncludeInAll { get; set; }

        /// <summary>
        /// Create a document property base.
        /// </summary>
        /// <param name="name">Sets the name of the property.</param>
        /// <param name="propertyType">Sets the property type.</param>
        public DocumentPropertyBase(string name, PropertyTypeEnum propertyType)
            : base(_INDEX_DEFAULT, _STORE_DEFAULT)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All document properties require a name.");
            if (propertyType == null)
                throw new ArgumentNullException("propertyType", "All document properties require a property type.");

            Name = name;
            PropertyType = propertyType;
            IndexName = name;
            IncludeInAll = _INCLUDE_IN_ALL_DEFAULT;
        }

        internal static void Serialize(DocumentPropertyBase prop, Dictionary<string, object> fieldDict)
        {
            if (prop == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_TYPE, prop.PropertyType.ToString(), _PROPERTY_TYPE_DEFAULT.ToString());
            MappingBase.Serialize(prop, fieldDict);
            fieldDict.AddObject(_INCLUDE_IN_ALL, prop.IncludeInAll, _INCLUDE_IN_ALL_DEFAULT);
            fieldDict.AddObject(_INDEX_NAME, prop.IndexName, prop.Name);
        }

        internal static void Deserialize(DocumentPropertyBase prop, Dictionary<string, object> fieldDict)
        {
            if (!fieldDict.Any())
                return;

            MappingBase.Deserialize(prop, fieldDict);
            prop.IncludeInAll = fieldDict.GetBool(_INCLUDE_IN_ALL, _INCLUDE_IN_ALL_DEFAULT);
            prop.IndexName = fieldDict.GetStringOrDefault(_INDEX_NAME);
            prop.PropertyType = PropertyTypeEnum.Find(fieldDict.GetString(_TYPE, _PROPERTY_TYPE_DEFAULT.ToString()));
        }
    }
}

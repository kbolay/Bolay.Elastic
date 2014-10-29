using Bolay.Elastic.Mapping.Types;
using Bolay.Elastic.Mapping.Types.Object;
using Bolay.Elastic.Mapping.Types.RootObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch.Mapping
{
    public class ModelMapping<T>
    {
        private const string _DELIMITER = ".";

        public Type TargetType { get { return typeof(T); } }
        public RootObjectProperty Mapping { get; private set; }
        public IEnumerable<PropertyMapping> PropertyMappings { get; private set; }

        public ModelMapping(RootObjectProperty documentTypeMapping)
        {
            Mapping = documentTypeMapping;
        }

        private IEnumerable<PropertyMapping> GetPropertyMappings(Type type, IEnumerable<IDocumentProperty> propertyMappings, string propertyPath = null) 
        {    
            List<PropertyMapping> result = new List<PropertyMapping>();

            if(!string.IsNullOrWhiteSpace(propertyPath))
            {
                propertyPath = propertyPath + ".";
            }

            foreach (PropertyInfo property in TargetType.GetProperties())
            {
                if (!property.IsPropertyIgnored())
                {
                    string propertyJsonName = property.GetJsonName();

                    PropertyMapping propMapping = new PropertyMapping();
                    propMapping.Property = property;                    
                    propMapping.Mapping = propertyMappings.First(x => x.Name.Equals(propertyJsonName, StringComparison.OrdinalIgnoreCase)) as DocumentPropertyBase;
                    propMapping.Path = propertyPath + propertyJsonName;
                    propMapping.ValueType = property.GetValueType();

                    result.Add(propMapping);

                    if (!propMapping.ValueType.IsPrimitiveType())
                    {
                        IEnumerable<PropertyMapping> subPropertyMappings = GetPropertyMappings(propMapping.ValueType,(propMapping.Mapping as ObjectProperty).Properties, propMapping.Path);

                        if (subPropertyMappings != null && subPropertyMappings.Any())
                        {
                            result.AddRange(subPropertyMappings);
                        }
                    }
                }                
            }

            return result;
        }
    }
}

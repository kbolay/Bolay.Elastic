using Bolay.Elastic.Mapping;
using Bolay.Elastic.Mapping.Properties;
using Bolay.Elastic.Mapping.Properties.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Linq2ElasticSearch.Mapping
{
    public class ModelMapping<T>
    {
        private const string _DELIMITER = ".";

        public Type TargetType { get { return typeof(T); } }
        public TypeMapping Mapping { get; private set; }
        public IEnumerable<PropertyMapping> PropertyMappings { get; private set; }

        public ModelMapping(TypeMapping documentTypeMapping)
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

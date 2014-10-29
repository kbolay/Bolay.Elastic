using Bolay.Elastic.Api.Mapping.Models;
//using Bolay.Elastic.Mapping.Types.RootObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    //public class MappingBuilder : IMappingBuilder
    //{
        //private const string _INDEX_NAME_DEFAULT = "object_index";
        //private const string _TYPE_NAME_DEFAULT = "object";

        //private readonly IConfigurationProvider _Configuration;
        //private string _IndexName { get; set; }
        //private string _TypeName { get; set; }

        //public string IndexName 
        //{
        //    get
        //    {
        //        if (!string.IsNullOrWhiteSpace(_IndexName))
        //            return _IndexName;
        //        return _INDEX_NAME_DEFAULT;
        //    }
        //    set
        //    {
        //        if (!string.IsNullOrWhiteSpace(value))
        //            _IndexName = value;
        //        else
        //            _IndexName = null;
        //    }
        //}
        //public string TypeName 
        //{
        //    get
        //    {
        //        if (!string.IsNullOrWhiteSpace(_TypeName))
        //            return _TypeName;
        //        return _TYPE_NAME_DEFAULT;
        //    }
        //    set
        //    {
        //        if (!string.IsNullOrWhiteSpace(value))
        //            _TypeName = value;
        //        else
        //            _TypeName = null;
        //    }
        //}

        //public MappingBuilder(IConfigurationProvider configurationProvider)
        //{
        //    if (configurationProvider == null)
        //        throw new ArgumentNullException("configurationProvider");

        //    _Configuration = configurationProvider;
        //}

        //public IndexMapping BuildMapping(string sampleJson)
        //{
        //    IndexMapping index = new IndexMapping();
        //    index.Name = IndexName;
        //    RootObjectProperty indexType = new RootObjectProperty();
        //    indexType.Name = TypeName;

        //    Dictionary<string, object> jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(sampleJson);
        //    IEnumerable<DocumentPropertyBase> props = RecurseObjectDict(jsonObject);
        //    if(props != null)
        //        indexType.Properties = new List<DocumentPropertyBase>(props);

        //    index.Types = new List<IndexType>();
        //    index.Types.Add(indexType);
        //    return index;
        //}

        //public Index BuildMapping(Type type)
        //{
        //    throw new NotImplementedException();
        //}

        //private IEnumerable<DocumentPropertyBase> RecurseObjectDict(Dictionary<string, object> objectDict, string path = null)
        //{
        //    if (objectDict == null || !objectDict.Any())
        //        return null;

        //    List<DocumentPropertyBase> properties = new List<DocumentPropertyBase>();
        //    foreach (KeyValuePair<string, object> property in objectDict)
        //    {
        //        bool isArray = property.Value.GetType() == typeof(JArray);
        //        JTokenType arrayType = JTokenType.Array;
        //        if (isArray)
        //            arrayType = (property.Value as JArray).Children().First().Type;

        //        if (property.Value is bool || (isArray && arrayType == JTokenType.Boolean))
        //            properties.Add(BuildBooleanProperty(property.Key, path));
        //        else if (property.Value is Int64 || (isArray && arrayType == JTokenType.Integer))
        //            properties.Add(BuildLongProperty(property.Key, path));
        //        else if (property.Value is string || (isArray && arrayType == JTokenType.String))
        //            properties.Add(BuildStringProperty(property.Key, path));
        //        else if (property.Value is Double || (isArray && arrayType == JTokenType.Float))
        //            properties.Add(BuildDoubleProperty(property.Key, path));
        //        else if (property.Value is JObject || (isArray && arrayType == JTokenType.Object))
        //        {
        //            ObjectProperty objectProp = null;
        //            if (!_Configuration.UseNestedObjects)
        //                objectProp = BuildObjectProperty(property.Key, path);
        //            else
        //                objectProp = BuildNestedProperty(property.Key, path);

        //            string tempPath = path;
        //            if (string.IsNullOrWhiteSpace(tempPath))
        //                tempPath = property.Key;
        //            else
        //                tempPath += "." + property.Key;

        //            Dictionary<string, object> childObject = null;

        //            if (isArray && arrayType == JTokenType.Object)
        //            {
        //                childObject = JsonConvert.DeserializeObject<Dictionary<string, object>>((property.Value as JArray).Children().First().ToString());
        //            }
        //            else
        //            {
        //                childObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(property.Value.ToString());
        //            }

        //            IEnumerable<DocumentPropertyBase> objectProperties = RecurseObjectDict(childObject, tempPath);
        //            if (objectProperties != null && objectProperties.Any())
        //                objectProp.Properties = new List<DocumentPropertyBase>(objectProperties);

        //            properties.Add(objectProp);
        //        }
        //        else
        //            throw new NotImplementedException("Help, I'm stuck!");                    
        //    }

        //    if (properties == null || !properties.Any())
        //        return null;

        //    return properties;
        //}

        //private BooleanProperty BuildBooleanProperty(string name, string path)
        //{
        //    string tempPath;
        //    if (string.IsNullOrWhiteSpace(path))
        //        tempPath = name;
        //    else
        //        tempPath = path + "." + name;

        //    Template template = _Configuration.FindTemplate(tempPath, PropertyType.Boolean);
        //    if (template != null)
        //    {
        //        return new BooleanProperty()
        //        {
        //            Boost = template.Boost,
        //            IncludeInAll = template.IncludeInAll,
        //            Index = template.Index,
        //            Name = name,
        //            Store = template.Store
        //        };
        //    }

        //    return new BooleanProperty()
        //    {
        //        Name = name
        //    };       
        //}
        //private DocumentPropertyBase BuildLongProperty(string name, string path)
        //{
        //    string tempPath;
        //    if (string.IsNullOrWhiteSpace(path))
        //        tempPath = name;
        //    else
        //        tempPath = path + "." + name;

        //    Template template = _Configuration.FindTemplate(tempPath, PropertyType.Long);
        //    if (template != null)
        //    {
        //        if (template.Analyzers != null && template.Analyzers.Any())
        //            return BuildMultiFieldProperty(name, template, PropertyType.Long);

        //        return new LongProperty()
        //        {
        //            Boost = template.Boost,
        //            IncludeInAll = template.IncludeInAll,
        //            Index = template.Index,
        //            Name = name,
        //            Store = template.Store
        //        };
        //    }

        //    return new LongProperty()
        //    {
        //        Name = name
        //    };
        //}
        //private DocumentPropertyBase BuildStringProperty(string name, string path)
        //{
        //    string tempPath;
        //    if (string.IsNullOrWhiteSpace(path))
        //        tempPath = name;
        //    else
        //        tempPath = path + "." + name;

        //    Template template = _Configuration.FindTemplate(tempPath, PropertyType.String);
        //    if (template != null)
        //    {
        //        if (template.Analyzers != null && template.Analyzers.Count > 1)
        //            return BuildMultiFieldProperty(name, template, PropertyType.String);

        //        StringProperty stringProp = new StringProperty()
        //        {
        //            Boost = template.Boost,
        //            IncludeInAll = template.IncludeInAll,
        //            Index = template.Index,
        //            Name = name,
        //            Store = template.Store,
        //        };

        //        if(template.Analyzers != null && template.Analyzers.Any())
        //        {
        //            stringProp.SearchAnalzyer = template.Analyzers.First().SearchAnalyzer;
        //            stringProp.IndexAnalyzer = template.Analyzers.First().IndexAnalyzer;
        //        }

        //        return stringProp;
        //    }

        //    return new StringProperty()
        //    {
        //        Name = name
        //    };
        //}
        //private DocumentPropertyBase BuildDoubleProperty(string name, string path)
        //{
        //    string tempPath;
        //    if (string.IsNullOrWhiteSpace(path))
        //        tempPath = name;
        //    else
        //        tempPath = path + "." + name;

        //    Template template = _Configuration.FindTemplate(tempPath, PropertyType.Double);
        //    if (template != null)
        //    {
        //        if (template.Analyzers != null && template.Analyzers.Any())
        //            return BuildMultiFieldProperty(name, template, PropertyType.Double);

        //        return new DoubleProperty()
        //        {
        //            Boost = template.Boost,
        //            IncludeInAll = template.IncludeInAll,
        //            Index = template.Index,
        //            Name = name,
        //            Store = template.Store
        //        };
        //    }

        //    return new DoubleProperty()
        //    {
        //        Name = name
        //    };
        //}
        //private ObjectProperty BuildObjectProperty(string name, string path)
        //{
        //    string tempPath;
        //    if (string.IsNullOrWhiteSpace(path))
        //        tempPath = name;
        //    else
        //        tempPath = path + "." + name;

        //    Template template = _Configuration.FindTemplate(tempPath, PropertyType.Object);
        //    if (template != null)
        //    {
        //        return new ObjectProperty()
        //        {
        //            IncludeInAll = template.IncludeInAll,
        //            Name = name
        //        };
        //    }

        //    return new ObjectProperty()
        //    {
        //        Name = name
        //    };
        //}
        //private NestedObjectProperty BuildNestedProperty(string name, string path)
        //{
        //    string tempPath;
        //    if (string.IsNullOrWhiteSpace(path))
        //        tempPath = name;
        //    else
        //        tempPath = path + "." + name;

        //    Template template = _Configuration.FindTemplate(tempPath, PropertyType.String);
        //    if(template != null)
        //    {
        //        return new NestedObjectProperty()
        //        {
        //            IncludeInAll = template.IncludeInAll,
        //            Name = name
        //        };
        //    }

        //    return new NestedObjectProperty()
        //    {
        //        Name = name
        //    };
        //}

        //private MultiFieldProperty BuildMultiFieldProperty(string name, Template template, PropertyType propertyType)
        //{
        //    MultiFieldProperty multi = new MultiFieldProperty()
        //    {
        //        Fields = new List<DocumentPropertyBase>(),
        //        Name = name,
        //        Type = PropertyType.MultiField.ToString()
        //    };

        //    if (propertyType == PropertyType.Long)
        //        multi.Fields.Add(new LongProperty()
        //            {
        //                Name = name,
        //                Boost = template.Boost,
        //                IncludeInAll = template.IncludeInAll,
        //                Index = template.Index,
        //                Store = template.Store
        //            });

        //    foreach (FieldAnalyzer analyzer in template.Analyzers)
        //    {
        //        multi.Fields.Add(new StringProperty()
        //            {
        //                Name = _Configuration.BuildFieldName(name, analyzer),
        //                Boost = template.Boost,
        //                IncludeInAll = template.IncludeInAll,
        //                Index = template.Index,
        //                IndexAnalyzer = analyzer.IndexAnalyzer,
        //                SearchAnalzyer = analyzer.SearchAnalyzer,
        //                Store = template.Store
        //            });
        //    }

        //    return multi;
        //}
    //}
}

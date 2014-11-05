using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch.Mapping
{
    public class PropertyMapping
    {
        public string Path { get; set; }
        public Type ValueType { get; set; }
        public PropertyInfo Property { get; set; }
        public DocumentPropertyBase Mapping { get; set; }
    }
}

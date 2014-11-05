using Bolay.Elastic.Api.Mapping.Models;
using Bolay.Elastic.Linq.RequestBuilder;
using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.Interfaces
{
    public interface IPropertyMappingResolver
    {
        //DocumentPropertyBase Resolve(PropertyInfo property, SearchType searchType);
        DocumentPropertyBase Resolve(string propertyPath);
    }
}

using Bolay.Elastic.Api.Mapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    public interface IConfigurationProvider
    {
        bool UseNestedObjects { get;}
        MultiFieldNamingConvention MultiFieldConventions { get; }
        List<Template> Templates { get; }

        //Template FindTemplate(string path, PropertyType propertyType);
        string BuildFieldName(string propertyName, FieldAnalyzer analyzer);
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    //public class ConfigurationProvider : IConfigurationProvider
    //{
        //public bool UseNestedObjects { get; set; }
        //public MultiFieldNamingConvention MultiFieldConventions { get; set; }
        //public List<Template> Templates { get; set; }

        //public ConfigurationProvider()
        //{
        //}

        //public static ConfigurationProvider DeserializeJson(string jsonConfig)
        //{
        //    return JsonConvert.DeserializeObject<ConfigurationProvider>(jsonConfig);
        //}

        //public Template FindTemplate(string path, Models.PropertyType propertyType)
        //{
        //    if (Templates == null || !Templates.Any())
        //        return null;

        //    IEnumerable<Template> validTemplates = Templates;
        //    if (path.Contains('.'))
        //        validTemplates = Templates.Where(x => x.PropertyPaths.Any(y => y == "*") || x.PropertyPaths.Any(y => path.Contains(y)));
        //    else
        //        validTemplates = Templates.Where(x => x.PropertyPaths.Contains("*"));

        //    validTemplates = validTemplates.Where(
        //                        x => x.Types.Contains("*") || 
        //                        x.Types.Any(y => y.Equals(propertyType.ToString(), StringComparison.OrdinalIgnoreCase)));

        //    validTemplates = validTemplates.OrderByDescending(x => x.PropertyPaths.Max(y => y.Length)).ThenByDescending(x => x.Types.Max(y => y.Length));

        //    if (validTemplates == null || !validTemplates.Any())
        //        return null;

        //    return validTemplates.First();
        //}

        //public string BuildFieldName(string propertyName, FieldAnalyzer analyzer)
        //{
        //    if (MultiFieldConventions == null)
        //        MultiFieldConventions = new MultiFieldNamingConvention();

        //    if (!string.IsNullOrWhiteSpace(analyzer.OverrideName))
        //        return analyzer.OverrideName;
        //    else if (MultiFieldConventions.PrependPropertyName)
        //    {
        //        if(analyzer != null && !string.IsNullOrWhiteSpace(analyzer.AnalyzerName))
        //            return propertyName + MultiFieldConventions.Separator + analyzer.AnalyzerName;

        //        return propertyName;
        //    }
                
        //    else
        //        return analyzer.AnalyzerName;
        //}
    //}
}

using Bolay.Elastic.Api.Mapping.Builder;
using Bolay.Elastic.Api.Mapping.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bolay.Elastic.Mvc4.ViewModels
{
    public class MappingTemplateViewModel
    {
        //public MappingTemplateViewModel()
        //{ 
        //    JsonConvert.DefaultSettings = () =>
        //    {
        //        return new JsonSerializerSettings()
        //        {
        //            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
        //            NullValueHandling = NullValueHandling.Ignore
        //        };
        //    };
        //}

        //[DataType(DataType.MultilineText)]
        //public string JsonTemplate { get; set; }

        //[DataType(DataType.MultilineText)]
        //public string SampleDocument { get; set; }

        //public string Build(IMappingBuilder mappingBuilder)
        //{
        //    if (mappingBuilder == null)
        //        return null;

        //    Index template = mappingBuilder.BuildMapping(SampleDocument);
        //    return JsonConvert.SerializeObject(template, Formatting.Indented);
        //}

        //public static string GetDefaultJsonTemplate()
        //{
        //    ConfigurationProvider defaultConfig = new ConfigurationProvider()
        //    {
        //        UseNestedObjects = false,
        //        MultiFieldConventions = new MultiFieldNamingConvention()
        //        {
        //            PrependPropertyName = true,
        //            Separator = "_"
        //        },
        //        Templates = new List<Template>()
        //        {
        //            new Template()
        //            {
        //                PropertyPaths = new List<string>()
        //                {
        //                    "*"
        //                },
        //                Types = new List<string>()
        //                {
        //                    "integer",
        //                    "long"
        //                },
        //                Analyzers = new List<FieldAnalyzer>()
        //                {
        //                    new FieldAnalyzer()
        //                    {
        //                        AnalyzerName = "edge_ngram_wildcard",
        //                        IndexAnalyzer = "edge_ngram_wildcard"
        //                    }
        //                }
        //            },
        //            new Template()
        //            {
        //                //PropertyPaths = new List<string>() { "*" },
        //                //Types = new List<string>(){"string"},
        //                Analyzers = new List<FieldAnalyzer>()
        //                {
        //                    new FieldAnalyzer()
        //                    {
        //                        AnalyzerName = "",
        //                        IndexAnalyzer = "standard"
        //                    },
        //                    new FieldAnalyzer()
        //                    {
        //                        AnalyzerName = "untouched",
        //                        IndexAnalyzer = "lowercase_untouched",
        //                        SearchAnalyzer = "lowercase_untouched"
        //                    },
        //                    new FieldAnalyzer()
        //                    {
        //                        AnalyzerName = "edge_ngram_wildcard",
        //                        IndexAnalyzer = "edge_ngram_wildcard"
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    return JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
        //}
    }
}
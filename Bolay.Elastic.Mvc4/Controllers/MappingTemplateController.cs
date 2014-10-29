using Bolay.Elastic.Api.Mapping.Builder;
using Bolay.Elastic.Mvc4.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bolay.Elastic.Mvc4.Controllers
{
    public class MappingTemplateController : Controller
    {
        //
        // GET: /MappingTemplate/
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    MappingTemplateViewModel mappingTemplate = new MappingTemplateViewModel()
        //    {
        //        JsonTemplate = MappingTemplateViewModel.GetDefaultJsonTemplate()
        //    };
        //    return View(mappingTemplate);
        //}

        /// <summary>
        /// POST: /MappingTemplate/BuildMapping/
        /// </summary>
        /// <param name="mappingTemplate"></param>
        /// <returns></returns>
        //[HttpPost]
        //public string BuildMapping(MappingTemplateViewModel mappingTemplate)
        //{
        //    ConfigurationProvider configProvider = JsonConvert.DeserializeObject<ConfigurationProvider>(mappingTemplate.JsonTemplate);

        //    IMappingBuilder builder = new MappingBuilder(configProvider);

        //    return mappingTemplate.Build(builder);
        //}
    }
}

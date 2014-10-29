using Bolay.Elastic.Api.Exceptions;
using Bolay.Elastic.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public static class ExceptionGenerator
    {
        public static ElasticRequestException GenerateException(HttpRequest request, HttpResponse response)
        {
            ElasticRequestException basicException = new ElasticRequestException(request, response);
            if (string.IsNullOrWhiteSpace(response.Body))
                return basicException;

            try
            {
                ElasticError error = JsonConvert.DeserializeObject<ElasticError>(response.Body);

                if (error == null)
                    return basicException;

                if (error.Status == 404 && error.Error.Contains("IndexMissingException"))
                {
                    int startIndex = error.Error.IndexOf("[[") + 2;
                    int endIndex = error.Error.IndexOf("]", startIndex);
                    string index = error.Error.Substring(startIndex, endIndex - startIndex);
                    return new IndexMissingException(index, request, response);
                }
            }
            catch { }           

            return basicException;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Exceptions
{
    public class ElasticRequestException : Exception
    {
        public readonly HttpRequest Request;
        public readonly HttpResponse Response;

        public ElasticRequestException(HttpRequest request, HttpResponse response) : this(request, response, null, null) { }
        public ElasticRequestException(HttpRequest request, HttpResponse response, string message) : this(request, response, message, null) { }
        public ElasticRequestException(HttpRequest request, HttpResponse response, string message, Exception innerException)
            : base(message, innerException)
        {
            Request = request;
            Response = response;
        }
    }
}

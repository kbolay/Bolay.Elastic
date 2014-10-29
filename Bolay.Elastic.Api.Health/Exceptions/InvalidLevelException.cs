using Bolay.Elastic.Api.Exceptions;
using Bolay.Elastic.Api.Health.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Health.Exceptions
{
    public class InvalidLevelException : ElasticRequestException
    {
        public InvalidLevelException(HttpRequest request, HttpResponse response) : base(request, response, null, null) { }
        public InvalidLevelException(HttpRequest request, HttpResponse response, string message) : base(request, response, message) { }
        public InvalidLevelException(HttpRequest request, HttpResponse response, string message, Exception innerException) : base(request, response, message, innerException) { }
    }
}

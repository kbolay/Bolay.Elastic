using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Body { get; private set; }
        public Dictionary<string, IEnumerable<string>> Headers { get; private set; }

        public HttpResponse(HttpResponseMessage httpResponse)
            : this( httpResponse.StatusCode, 
                    httpResponse.Content.ReadAsStringAsync().Result, 
                    httpResponse.GetHeaders())
        {}

        public HttpResponse(HttpStatusCode statusCode, string responseBody = null, Dictionary<string, IEnumerable<string>> headers = null)
        {
            this.StatusCode = statusCode;
            this.Body = responseBody;
            this.Headers = headers;
        }
    }
}

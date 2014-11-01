using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public static class HttpClientExtensions
    {
        public static void AddHeaders(this HttpClient client, Dictionary<string, string> headers = null)
        {
            if (client == null)
                throw new ArgumentNullException("client", "Adding headers to an http client requires the client.");

            if (headers == null || !headers.Any())
                return;

            // TODO: figure out if i need to clear my headers
            // client.DefaultRequestHeaders.Clear();

            foreach (KeyValuePair<string, string> header in headers)
            {
                // TODO: this may get angry if i add the "wrong" headers
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public static void AddHeaders(this HttpRequestMessage request, Dictionary<string, string> headers = null)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (headers == null || !headers.Any())
                return;

            foreach (KeyValuePair<string, string> header in headers)
            {
                // TODO: this may get angry if i add the "wrong" headers
                request.Headers.Add(header.Key, header.Value);
            }
        }

        public static Dictionary<string, IEnumerable<string>> GetHeaders(this HttpResponseMessage httpResponse)
        {
            if (httpResponse.Headers == null || !httpResponse.Headers.Any())
                return null;

            Dictionary<string, IEnumerable<string>> headers = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase);

            foreach (var responseHeader in httpResponse.Headers)
            {
                headers.Add(responseHeader.Key, responseHeader.Value);
            }


            if (!headers.Any())
                return null;

            return headers;
        }
    }
}

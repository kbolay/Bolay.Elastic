using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api
{
    public class HttpRequest
    {
        public Uri Uri { get; private set; }
        public object Content { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }

        public HttpRequest(string uri, object content = null, Dictionary<string, string> headers = null)
            : this(new Uri(uri), content, headers)
        { }

        public HttpRequest(Uri uri, object content = null, Dictionary<string, string> headers = null)
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "Http Request requires a uri.");

            this.Uri = uri;
            this.Content = content;
            this.Headers = headers;
        }

        public static StringBuilder AddToQueryString(StringBuilder queryString, string key, string value = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "A query string piece requires a key.");

            if (queryString == null)
                queryString = new StringBuilder();

            if (queryString.Length == 0)
                queryString.Append("?");

            if (queryString.Length > 1)
                queryString.Append("&");

            queryString.Append(key);

            if (!string.IsNullOrWhiteSpace(value))
            {
                queryString.Append("=");
                queryString.Append(value);
            }

            return queryString;
        }
    }
}

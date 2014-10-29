using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Exceptions
{
    public class IndexMissingException : ElasticRequestException
    {
        public string Index { get; private set; }

        public IndexMissingException(HttpRequest request, HttpResponse response) 
            : this(GetIndexName(response.Body), request, response) 
        { }
        public IndexMissingException(string index, HttpRequest request, HttpResponse response)
            : base(request, response)
        {
            if (string.IsNullOrWhiteSpace(index))
                throw new ArgumentNullException("index", "IndexMissingException requires the index name.");

            Index = index;
        }

        private static string GetIndexName(string body)
        {
            int startIndex = body.IndexOf("[[") + 2;
            int endIndex = body.IndexOf("]", startIndex);
            return body.Substring(startIndex, endIndex - startIndex);
        }
    }
}

using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic
{
    public class ElasticUriProvider : IElasticUriProvider
    {
        private Uri _Uri { get; set; }
        public Uri Uri { get { return _Uri; } }
        public Uri ClusterUri
        {
            get 
            {
                return new Uri(_Uri.GetLeftPart(UriPartial.Authority));
            }
        }

        public ElasticUriProvider(string uri) : this(new Uri(uri)) { }
        public ElasticUriProvider(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            _Uri = uri;
        }
    }
}
